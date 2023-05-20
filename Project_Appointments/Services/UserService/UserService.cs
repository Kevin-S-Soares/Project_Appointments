using Microsoft.IdentityModel.Tokens;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Requests;
using Project_Appointments.Services.EmailService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Project_Appointments.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IEmailService _emailService;

        public UserService(ApplicationContext context,
            IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public ServiceResponse<UserRegisterRequest> Register(UserRegisterRequest request)
        {
            bool condition = _context.Users.Any(x => x.Email == request.Email);
            if (condition is true)
            {
                return new(errorMessage: "User already exists",
                    statusCode: StatusCodes.Status409Conflict);
            }
            var user = CreateUser(request);
            _context.Users.Add(user);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _emailService.SendEmail(EmailFactory.CreateEmailFromNewUser(user));
            return new(data: request, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<UserRegisterRequest>>
            RegisterAsync(UserRegisterRequest request)
        {
            bool condition = _context.Users.Any(x => x.Email == request.Email);
            if (condition is true)
            {
                return new(errorMessage: "User already exists",
                    statusCode: StatusCodes.Status409Conflict);
            }
            var user = CreateUser(request);
            await _context.Users.AddAsync(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            _ = _emailService.SendEmailAsync(
                EmailFactory.CreateEmailFromNewUser(user));
            return new(data: request, statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<string> Verify(string token)
        {
            var query = _context.Users.FirstOrDefault(x => x.VerificationToken == token);
            if (query is null)
            {
                return new(errorMessage: "Invalid token",
                    statusCode: StatusCodes.Status400BadRequest);
            }
            query.VerifiedAt = DateTime.UtcNow;
            query.VerificationToken = string.Empty;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Token verified!", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> VerifyAsync(string token)
        {
            var query = _context.Users.FirstOrDefault(x => x.VerificationToken == token);
            if (query is null)
            {
                return new(errorMessage: "Invalid token",
                    statusCode: StatusCodes.Status400BadRequest);
            }
            query.VerifiedAt = DateTime.UtcNow;
            query.VerificationToken = null;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Token verified!", statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<string> ForgetPassword(string email)
        {
            var query = _context.Users.FirstOrDefault(x => x.Email == email);
            if (query is not null)
            {
                query.PasswordResetToken = CreateRandomToken();
                query.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return new(errorMessage: e.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }
                _emailService.SendEmail(new());
            }
            return new(data: "Verify your email", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> ForgetPasswordAsync(string email)
        {
            var query = _context.Users.FirstOrDefault(x => x.Email == email);
            if (query is not null)
            {
                query.PasswordResetToken = CreateRandomToken();
                query.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    return new(errorMessage: e.Message,
                        statusCode: StatusCodes.Status500InternalServerError);
                }
                _ = _emailService.SendEmailAsync(new());
            }
            return new(data: "Verify your email", statusCode: StatusCodes.Status200OK);
        }

        private User CreateUser(UserRegisterRequest userRegisterRequest)
        {
            CreatePassword(
                userRegisterRequest.Password,
                out var salt,
                out var hash);

            var token = CreateRandomToken();

            userRegisterRequest.Password = string.Empty;

            return new()
            {
                Email = userRegisterRequest.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                VerificationToken = token
            };
        }

        private void CreatePassword(string password,
            out byte[] passwordSalt, out byte[] passwordHash)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac
                .ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private string CreateRandomToken()
        {
            /*
            while (true)
            {
                var result = RandomNumberGenerator.GetBytes(64);
                bool condition = _context.Users.Any(x => x.VerificationToken == result);
                if(condition is false)
                {
                    return result;
                }
            }
            */
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }

        public ServiceResponse<string> ResetPassword(UserResetPasswordRequest request)
        {
            var query = _context.Users.FirstOrDefault(x => x.PasswordResetToken == request.Token);
            if (query is null)
            {
                return new(errorMessage: "Invalid token",
                    statusCode: StatusCodes.Status400BadRequest);
            }
            if (query.ResetTokenExpires > DateTime.UtcNow)
            {
                return new(errorMessage: "Expired token",
                    statusCode: StatusCodes.Status409Conflict);
            }
            CreatePassword(request.Password,
                out var salt,
                out var hash);
            query.PasswordSalt = salt;
            query.PasswordHash = hash;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Password reseted", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> ResetPasswordAsync(UserResetPasswordRequest request)
        {
            var query = _context.Users.FirstOrDefault(x => x.PasswordResetToken == request.Token);
            if (query is null)
            {
                return new(errorMessage: "Invalid token",
                    statusCode: StatusCodes.Status400BadRequest);
            }
            CreatePassword(request.Password,
                out var salt,
                out var hash);
            query.PasswordSalt = salt;
            query.PasswordHash = hash;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Password reseted", statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<string> Login(UserLoginRequest request)
        {
            var query = _context.Users.FirstOrDefault(x => x.Email == request.Email);
            bool condition = query is not null
                && VerifyPasswordHash(request.Password, query.PasswordHash, query.PasswordSalt);
            if (condition is false)
            {
                return new(errorMessage: "Email invalid or password invalid",
                    statusCode: StatusCodes.Status400BadRequest);
            }
            if (query!.VerifiedAt is null)
            {
                return new(errorMessage: "Email not verified",
                    statusCode: StatusCodes.Status409Conflict);
            }
            var data = CreateJWT(user: query);
            return new(data: data, statusCode: StatusCodes.Status200OK);
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        private string CreateJWT(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Actor, user.OdontologistId.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable("secret_key")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
