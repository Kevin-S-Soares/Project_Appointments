using Server.Models;

namespace Server.Services.BreakTimeService
{
    public interface IBreakTimeService
    {
        public ServiceResponse<BreakTime> Create(BreakTime breakTime);
        public Task<ServiceResponse<BreakTime>> CreateAsync(BreakTime breakTime);
        public ServiceResponse<BreakTime> FindById(long id);
        public ServiceResponse<IEnumerable<BreakTime>> FindAll();
        public ServiceResponse<BreakTime> Update(BreakTime breakTime);
        public Task<ServiceResponse<BreakTime>> UpdateAsync(BreakTime breakTime);
        public ServiceResponse<string> Delete(long id);
        public Task<ServiceResponse<string>> DeleteAsync(long id);
    }
}
