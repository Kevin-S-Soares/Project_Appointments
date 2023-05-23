﻿using Server.Models;

namespace Server.Services.ScheduleService
{
    public interface IScheduleService
    {
        public ServiceResponse<Schedule> Create(Schedule schedule);
        public Task<ServiceResponse<Schedule>> CreateAsync(Schedule schedule);
        public ServiceResponse<Schedule> FindById(long id);
        public ServiceResponse<IEnumerable<Schedule>> FindAll();
        public ServiceResponse<Schedule> Update(Schedule schedule);
        public Task<ServiceResponse<Schedule>> UpdateAsync(Schedule schedule);
        public ServiceResponse<string> Delete(long id);
        public Task<ServiceResponse<string>> DeleteAsync(long id);
    }
}
