using Server.Models;

namespace Server.Services.DetailedOdontologistService
{
    public interface IDetailedOdontologistService
    {
        public ServiceResponse<DetailedOdontologist> FindById(long id);
        public ServiceResponse<IEnumerable<DetailedOdontologist>> FindAll();
    }
}
