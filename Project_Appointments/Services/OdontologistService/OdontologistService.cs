using Project_Appointments.Contexts;
using Project_Appointments.Models.Exceptions;

namespace Project_Appointments.Models.Services
{
    public class OdontologistService
    {
        private readonly ApplicationContext _context;
        public OdontologistService(ApplicationContext context)
        {
            _context = context;
        }

        public Odontologist Add(Odontologist odontologist)
        {
            _context.Odontologists.Add(odontologist);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception adding odontologist: " + e.Message);
            }
            return odontologist;
        }

        public void Update(Odontologist odontologist)
        {

            try
            {
                Odontologist result = FindById(odontologist.Id);
                _context.Odontologists.Update(odontologist);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception deleting odontologist: Odontologist not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception updating odontologist: " + e.Message);
            }
        }

        public void Delete(long id)
        {
            try
            {
                Odontologist result = FindById(id);
                _context.Odontologists.Remove(result);
                _context.SaveChanges();
            }
            catch (ServiceException)
            {
                throw new ServiceException("Exception deleting odontologist: Odontologist not found");
            }
            catch (Exception e)
            {
                throw new ServiceException("Exception deleting odontologist: " + e.Message);
            }
        }

        public Odontologist FindById(long id)
        {
            var result = _context.Odontologists.FirstOrDefault(x => x.Id == id);
            return result is null ? throw new ServiceException("Exception: Odontologist not found") : result;
        }

        public ICollection<Odontologist> FindAll()
        {
            return _context.Odontologists.ToList();
        }
    }
}
