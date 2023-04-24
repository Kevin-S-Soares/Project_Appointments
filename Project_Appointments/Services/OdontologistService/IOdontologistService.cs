using Project_Appointments.Models;

namespace Project_Appointments.Services.OdontologistService
{
    public interface IOdontologistService
    {
        public ServiceResponse Create(Odontologist odontologist);
        public Task<ServiceResponse> CreateAsync(Odontologist odontologist);
        public ServiceResponse FindById(long id);
        public ServiceResponse FindAll();
        public ServiceResponse Update(Odontologist odontologist);
        public Task<ServiceResponse> UpdateAsync(Odontologist odontologist);
        public ServiceResponse Delete(long id);
        public Task<ServiceResponse> DeleteAsync(long id);
    }
}
