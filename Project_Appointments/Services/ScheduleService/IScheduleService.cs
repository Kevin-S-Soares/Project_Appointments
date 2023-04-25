using Project_Appointments.Models;

namespace Project_Appointments.Services.ScheduleService
{
    public interface IScheduleService
    {
        public ServiceResponse Create(Schedule schedule);
        public Task<ServiceResponse> CreateAsync(Schedule schedule);
        public ServiceResponse FindById(long id);
        public ServiceResponse FindAll();
        public ServiceResponse FindAllFromOdontologistId(long id);
        public ServiceResponse Update(Schedule schedule);
        public Task<ServiceResponse> UpdateAsync(Schedule schedule);
        public ServiceResponse Delete(long id);
        public Task<ServiceResponse> DeleteAsync(long id);
    }
}
