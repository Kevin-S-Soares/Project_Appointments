using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Requests;
using Project_Appointments.Services.EmailService;
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

        public ServiceResponse<UserRegisterRequest> Add(UserRegisterRequest userRegisterRequest)
        {
            bool condition = _context.Users.Any(x => x.Email == userRegisterRequest.Email);
            if(condition is true)
            {
                return new(errorMessage: "User already exists", 
                    statusCode: StatusCodes.Status409Conflict);
            }
            var user = CreateUser(userRegisterRequest);
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
            _emailService.SendEmail(new());
            return new(data: userRegisterRequest, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<UserRegisterRequest>> AddAsync(UserRegisterRequest userRegisterRequest)
        {
            bool condition = _context.Users.Any(x => x.Email == userRegisterRequest.Email);
            if (condition is true)
            {
                return new(errorMessage: "User already exists",
                    statusCode: StatusCodes.Status409Conflict);
            }
            var user = CreateUser(userRegisterRequest);
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
            await _emailService.SendEmailAsync(new());
            return new(data: userRegisterRequest, statusCode: StatusCodes.Status201Created);
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

        private byte[] CreateRandomToken()
        {
            while (true)
            {
                var result = RandomNumberGenerator.GetBytes(64);
                bool condition = _context.Users.Any(x => x.VerificationToken == result);
                if(condition is false)
                {
                    return result;
                }
            }
        }
    }
}
