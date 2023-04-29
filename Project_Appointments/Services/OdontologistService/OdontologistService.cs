using Project_Appointments.Contexts;
using Project_Appointments.Models;

namespace Project_Appointments.Services.OdontologistService
{
    public class OdontologistService : IOdontologistService
    {
        private readonly ApplicationContext _context;
        public OdontologistService(ApplicationContext context)
        {
            _context = context;
        }

        public ServiceResponse<Odontologist> Create(Odontologist odontologist)
        {
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
            return new(data: _context.Odontologists,
                statusCode: StatusCodes.Status200OK);
        }

        public ServiceResponse<Odontologist> FindById(long id)
        {
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
    }
}
