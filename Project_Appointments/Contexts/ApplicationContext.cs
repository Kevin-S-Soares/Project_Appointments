using Microsoft.EntityFrameworkCore;
using Project_Appointments.Models;

namespace Project_Appointments.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Odontologist> Odontologists { get; set; } = default!;
        public DbSet<Appointment> Appointments { get; set; } = default!;
        public DbSet<Schedule> Schedules { get; set; } = default!;
        public DbSet<BreakTime> BreakTimes { get; set; } = default!;

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
