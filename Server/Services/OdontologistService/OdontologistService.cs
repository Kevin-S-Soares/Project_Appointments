using Server.Contexts;
using Server.Models;
using Server.Services.AuthService;

namespace Server.Services.OdontologistService
{
    public class OdontologistService : IOdontologistService
    {
        private readonly ApplicationContext _context;
        private readonly IAuthService _authService;

        public OdontologistService(ApplicationContext context,
            IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public ServiceResponse<Odontologist> Create(Odontologist odontologist)
        {
            if (IsAuthorizedToCreate() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            _context.Odontologists.Add(odontologist);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: odontologist, statusCode: StatusCodes.Status201Created);
        }

        public async Task<ServiceResponse<Odontologist>> CreateAsync(Odontologist odontologist)
        {
            if (IsAuthorizedToCreate() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            _context.Odontologists.Add(odontologist);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: odontologist, statusCode: StatusCodes.Status201Created);
        }

        public ServiceResponse<string> Delete(long id)
        {
            if (IsAuthorizedToDelete() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Odontologist does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Odontologists.Remove(query);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Odontologist deleted", statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<string>> DeleteAsync(long id)
        {
            if (IsAuthorizedToDelete() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Odontologist does not exist",
                    statusCode: StatusCodes.Status404NotFound);
            }
            _context.Odontologists.Remove(query);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: "Odontologist deleted", statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<IEnumerable<Odontologist>> FindAll()
        {
            if (IsAuthorizedToReadAll() is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var result = _context.Odontologists;
            return new(data: result, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Odontologist> FindById(long id)
        {
            if (IsAuthorizedToRead(resourceId: id) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if (query is null)
            {
                return new(errorMessage: "Odontologist does not exist",
                statusCode: StatusCodes.Status404NotFound);
            }
            return new(data: query, statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Odontologist> Update(Odontologist odontologist)
        {
            if (IsAuthorizedToUpdate(resourceId: odontologist.Id) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            bool condition = _context.Odontologists.Any(x => x.Id == odontologist.Id);
            if (condition is false)
            {
                return new(errorMessage: "Odontologist does not exist",
                statusCode: StatusCodes.Status404NotFound);
            }
            try
            {
                _context.Odontologists.Update(odontologist);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: odontologist, statusCode: StatusCodes.Status200OK);
        }

        public async Task<ServiceResponse<Odontologist>> UpdateAsync(Odontologist odontologist)
        {
            if (IsAuthorizedToUpdate(resourceId: odontologist.Id) is false)
            {
                return new(errorMessage: "Not authorized",
                    statusCode: StatusCodes.Status403Forbidden);
            }
            bool condition = _context.Odontologists.Any(x => x.Id == odontologist.Id);
            if (condition is false)
            {
                return new(errorMessage: "Odontologist does not exist",
                statusCode: StatusCodes.Status404NotFound);
            }
            try
            {
                _context.Odontologists.Update(odontologist);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return new(errorMessage: e.Message,
                statusCode: StatusCodes.Status500InternalServerError);
            }
            return new(data: odontologist, statusCode: StatusCodes.Status200OK);
        }

        private bool IsAuthorizedToCreate()
        {
            return _authService.IsAdmin();
        }
        private bool IsAuthorizedToRead(long resourceId)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId) || _authService.IsAttendant();
        }
        private bool IsAuthorizedToReadAll()
        {
            return _authService.IsAdmin() || _authService.IsAttendant();
        }
        private bool IsAuthorizedToUpdate(long resourceId)
        {
            return _authService.IsAdmin() || _authService.IsOdontologist(resourceId);
        }
        private bool IsAuthorizedToDelete()
        {
            return _authService.IsAdmin();
        }
    }
}
