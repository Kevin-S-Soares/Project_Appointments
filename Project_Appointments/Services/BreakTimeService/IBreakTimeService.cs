using Project_Appointments.Models;

namespace Project_Appointments.Services.BreakTimeService
{
    public interface IBreakTimeService
    {
        public ServiceResponse<BreakTime> Create(BreakTime breakTime);
        public Task<ServiceResponse<BreakTime>> CreateAsync(BreakTime breakTime);
        public ServiceResponse<BreakTime> FindById(long id);
        public ServiceResponse<IEnumerable<BreakTime>> FindAll();
        public ServiceResponse<IEnumerable<BreakTime>> FindAllFromSameSchedule(BreakTime breakTime);
        public ServiceResponse<IEnumerable<BreakTime>> FindAllFromSameSchedule(Appointment appointment);
        public ServiceResponse<BreakTime> Update(BreakTime breakTime);
        public Task<ServiceResponse<BreakTime>> UpdateAsync(BreakTime breakTime);
        public ServiceResponse<string> Delete(long id);
        public Task<ServiceResponse<string>> DeleteAsync(long id);
    }
}
