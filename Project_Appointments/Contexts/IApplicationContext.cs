using Microsoft.EntityFrameworkCore;
using Project_Appointments.Models;

namespace Project_Appointments.Contexts
{
    public interface IApplicationContext
    {
        public DbSet<Odontologist> Odontologists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<BreakTime> BreakTimes { get; set; }
    }
}
