using Project_Appointments.Models;

namespace Project_Appointments.Services.DetailedOdontologistService
{
    public interface IDetailedOdontologistService
    {
        public ServiceResponse<DetailedOdontologist> FindById(long id);
        public ServiceResponse<IEnumerable<DetailedOdontologist>> FindAll();
    }
}
