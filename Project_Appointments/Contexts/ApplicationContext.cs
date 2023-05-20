using Microsoft.EntityFrameworkCore;
using Project_Appointments.Models;
using Project_Appointments.Models.ContextModels;

namespace Project_Appointments.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        public virtual DbSet<ContextOdontologist> Odontologists { get; set; }
        public virtual DbSet<ContextAppointment> Appointments { get; set; }
        public virtual DbSet<ContextSchedule> Schedules { get; set; }
        public virtual DbSet<ContextBreakTime> BreakTimes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string? connectionString = Environment.GetEnvironmentVariable("sqlserver");
            if (connectionString is not null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
