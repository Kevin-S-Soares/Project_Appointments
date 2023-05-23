using Server.Models;

namespace Server.Services.AppointmentService
{
    public interface IAppointmentService
    {
        public ServiceResponse<Appointment> Create(Appointment appointment);
        public Task<ServiceResponse<Appointment>> CreateAsync(Appointment appointment);
        public ServiceResponse<Appointment> FindById(long id);
        public ServiceResponse<IEnumerable<Appointment>> FindAll();
        public ServiceResponse<Appointment> Update(Appointment appointment);
        public Task<ServiceResponse<Appointment>> UpdateAsync(Appointment appointment);
        public ServiceResponse<string> Delete(long id);
        public Task<ServiceResponse<string>> DeleteAsync(long id);
    }
}
