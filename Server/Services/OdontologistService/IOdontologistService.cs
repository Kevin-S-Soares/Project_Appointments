using Server.Models;

namespace Server.Services.OdontologistService
{
    public interface IOdontologistService
    {
        public ServiceResponse<Odontologist> Create(Odontologist odontologist);
        public Task<ServiceResponse<Odontologist>> CreateAsync(Odontologist odontologist);
        public ServiceResponse<Odontologist> FindById(long id);
        public ServiceResponse<IEnumerable<Odontologist>> FindAll();
        public ServiceResponse<Odontologist> Update(Odontologist odontologist);
        public Task<ServiceResponse<Odontologist>> UpdateAsync(Odontologist odontologist);
        public ServiceResponse<string> Delete(long id);
        public Task<ServiceResponse<string>> DeleteAsync(long id);
    }
}
