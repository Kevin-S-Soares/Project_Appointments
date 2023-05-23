using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Contexts
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Odontologist>().HasData(
                new Odontologist()
                {
                    Id = 1,
                    Name = "John Blue",
                    Email = "john@blue.com",
                    Phone = "(011) 91111-1111"
                },
                new Odontologist()
                {
                    Id = 2,
                    Name = "Maria Yellow",
                    Email = "maria@yellow.com",
                    Phone = "(011) 92222-2222"
                }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule()
                {
                    Id = 1,
                    Name = "Monday",
                    OdontologistId = 1,
                    StartDay = DayOfWeek.Monday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndDay = DayOfWeek.Monday,
                    EndTime = new TimeSpan(18, 0, 0)

                },
                new Schedule()
                {
                    Id = 2,
                    Name = "Tuesday",
                    OdontologistId = 1,
                    StartDay = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(9, 0, 0),
                    EndDay = DayOfWeek.Tuesday,
                    EndTime = new TimeSpan(18, 0, 0)
                },
                new Schedule()
                {
                    Id = 3,
                    Name = "Wednesday-Thursday",
                    OdontologistId = 2,
                    StartDay = DayOfWeek.Wednesday,
                    StartTime = new TimeSpan(21, 0, 0),
                    EndDay = DayOfWeek.Thursday,
                    EndTime = new TimeSpan(6, 0, 0)
                },
                new Schedule()
                {
                    Id = 4,
                    Name = "Thursday-Friday",
                    OdontologistId = 2,
                    StartDay = DayOfWeek.Thursday,
                    StartTime = new TimeSpan(21, 0, 0),
                    EndDay = DayOfWeek.Friday,
                    EndTime = new TimeSpan(6, 0, 0)
                }
            );

            modelBuilder.Entity<BreakTime>().HasData(
                new BreakTime()
                {
                    Id = 1,
                    ScheduleId = 1,
                    Name = "Lunch",
                    StartDay = DayOfWeek.Monday,
                    StartTime = new TimeSpan(12, 0, 0),
                    EndDay = DayOfWeek.Monday,
                    EndTime = new TimeSpan(13, 0, 0)
                },
                new BreakTime()
                {
                    Id = 2,
                    ScheduleId = 2,
                    Name = "Lunch",
                    StartDay = DayOfWeek.Tuesday,
                    StartTime = new TimeSpan(12, 0, 0),
                    EndDay = DayOfWeek.Tuesday,
                    EndTime = new TimeSpan(13, 0, 0)
                },
                new BreakTime()
                {
                    Id = 3,
                    ScheduleId = 3,
                    Name = "Dinner",
                    StartDay = DayOfWeek.Thursday,
                    StartTime = new TimeSpan(0, 0, 0),
                    EndDay = DayOfWeek.Thursday,
                    EndTime = new TimeSpan(1, 0, 0)
                },
                new BreakTime()
                {
                    Id = 4,
                    ScheduleId = 4,
                    Name = "Dinner",
                    StartDay = DayOfWeek.Friday,
                    StartTime = new TimeSpan(0, 0, 0),
                    EndDay = DayOfWeek.Friday,
                    EndTime = new TimeSpan(1, 0, 0)
                }
            );

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment()
                {
                    Id = 1,
                    ScheduleId = 1,
                    Start = new DateTime(2023, 1, 2, 9, 0, 0),
                    End = new DateTime(2023, 1, 2, 9, 15, 0),
                    PatientName = "Bob Brown",
                    Description = "Cleaning"
                },
                new Appointment()
                {
                    Id = 2,
                    ScheduleId = 2,
                    Start = new DateTime(2023, 1, 3, 9, 0, 0),
                    End = new DateTime(2023, 1, 3, 9, 15, 0),
                    PatientName = "Anna Red",
                    Description = "Cleaning"
                },
                new Appointment()
                {
                    Id = 3,
                    ScheduleId = 3,
                    Start = new DateTime(2023, 1, 4, 21, 0, 0),
                    End = new DateTime(2023, 1, 4, 21, 15, 0),
                    PatientName = "Peter Green",
                    Description = "Treatment"
                },
                new Appointment()
                {
                    Id = 4,
                    ScheduleId = 4,
                    Start = new DateTime(2023, 1, 5, 21, 0, 0),
                    End = new DateTime(2023, 1, 5, 21, 15, 0),
                    PatientName = "Lara White",
                    Description = "Treatment"
                }
            );
        }
    }
}
