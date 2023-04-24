using Microsoft.EntityFrameworkCore;
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

        public ServiceResponse Create(Odontologist odontologist)
        {
            var result = new ServiceResponse(value: odontologist);
            _context.Odontologists.Add(odontologist);
            try
            {
                _context.SaveChanges();
                result.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception e) 
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> CreateAsync(Odontologist odontologist)
        {
            var result = new ServiceResponse(value: odontologist);
            try
            {
                await _context.Odontologists.AddAsync(odontologist);
                await _context.SaveChangesAsync();
                result.StatusCode = StatusCodes.Status201Created;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public ServiceResponse Delete(long id)
        {
            var result = new ServiceResponse();
            var obj = FindById(id).Value;
            if (obj is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Odontologist does not exist";
                return result;
            }
            _context.Odontologists.Remove((Odontologist) obj);
            try
            {
                _context.SaveChanges();
                result.Value = "Odontologist deleted";
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> DeleteAsync(long id)
        {
            var result = new ServiceResponse();
            var obj = FindById(id).Value;
            if (obj is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Odontologist does not exist";
                return result;
            }
            _context.Odontologists.Remove((Odontologist) obj);
            try
            {
                await _context.SaveChangesAsync();
                result.Value = "Odontologist deleted";
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public ServiceResponse FindAll()
        {
            var result = new ServiceResponse(_context.Odontologists);
            return result;
        }

        public ServiceResponse FindById(long id)
        {
            var result = new ServiceResponse();
            result.Value = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            if(result.Value is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Odontologist does not exist";
            }
            return result;
        }

        public ServiceResponse Update(Odontologist odontologist)
        {
            var result = new ServiceResponse();
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == odontologist.Id);
            if (query is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Odontologist does not exist";
                return result;
            }
            _context.Entry(query).State = EntityState.Detached;
            try
            {
                _context.Odontologists.Update(odontologist);
                _context.SaveChanges();
                result.Value = odontologist;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }

        public async Task<ServiceResponse> UpdateAsync(Odontologist odontologist)
        {
            var result = new ServiceResponse();
            var query = _context.Odontologists.FirstOrDefault(x => x.Id == odontologist.Id);
            if (query is null)
            {
                result.StatusCode = StatusCodes.Status404NotFound;
                result.Value = "Odontologist does not exist";
                return result;
            }
            _context.Entry(query).State = EntityState.Detached;
            try
            {
                _context.Odontologists.Update(odontologist);
                await _context.SaveChangesAsync();
                result.Value = odontologist;
            }
            catch (Exception e)
            {
                result.StatusCode = StatusCodes.Status500InternalServerError;
                result.Value = e.Message;
            }
            return result;
        }
    }
}
