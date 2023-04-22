using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelTests.Services
{
    [TestClass]
    public class AppointmentServiceTests
    {
        private AppointmentService _model = default!;

        private readonly IQueryable<BreakTime> _dataBreakTime = new List<BreakTime>()
        {
            new()
            {
                Id = 1L,
                ScheduleId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(12, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(13, 0, 0)
            }
        }.AsQueryable();

        private readonly IQueryable<Schedule> _dataSchedule = new List<Schedule>()
        {
            new()
            {
                Id = 1L,
                OdontologistId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(21, 0, 0),
            }
        }.AsQueryable();

        private readonly IQueryable<Appointment> _dataAppointment = new List<Appointment>()
        {
            new()
            {
                Id = 1L,
                ScheduleId = 1L,
                Start = new DateTime(2023, 1, 2, 9, 0, 0),
                End = new DateTime(2023, 1, 2, 9, 14, 0),
                PatientName = "Test test",
                Description = "I am testing"
            }
        }.AsQueryable();

        [TestInitialize]
        public void Setup()
        {
            var mockBreakTimeSet = new Mock<DbSet<BreakTime>>();
            mockBreakTimeSet.As<IQueryable<BreakTime>>().Setup(x => x.Provider)
                .Returns(_dataBreakTime.Provider);
            mockBreakTimeSet.As<IQueryable<BreakTime>>().Setup(x => x.Expression)
                .Returns(_dataBreakTime.Expression);
            mockBreakTimeSet.As<IQueryable<BreakTime>>().Setup(x => x.ElementType)
                .Returns(_dataBreakTime.ElementType);
            mockBreakTimeSet.As<IQueryable<BreakTime>>().Setup(x => x.GetEnumerator())
                .Returns(_dataBreakTime.GetEnumerator());

            var mockScheduleSet = new Mock<DbSet<Schedule>>();
            mockScheduleSet.As<IQueryable<Schedule>>().Setup(x => x.Provider)
                .Returns(_dataSchedule.Provider);
            mockScheduleSet.As<IQueryable<Schedule>>().Setup(x => x.Expression)
                .Returns(_dataSchedule.Expression);
            mockScheduleSet.As<IQueryable<Schedule>>().Setup(x => x.ElementType)
                .Returns(_dataSchedule.ElementType);
            mockScheduleSet.As<IQueryable<Schedule>>().Setup(x => x.GetEnumerator())
                .Returns(_dataSchedule.GetEnumerator());

            var mockAppointmentSet = new Mock<DbSet<Appointment>>();
            mockAppointmentSet.As<IQueryable<Appointment>>().Setup(x => x.Provider)
                .Returns(_dataAppointment.Provider);
            mockAppointmentSet.As<IQueryable<Appointment>>().Setup(x => x.Expression)
                .Returns(_dataAppointment.Expression);
            mockAppointmentSet.As<IQueryable<Appointment>>().Setup(x => x.ElementType)
                .Returns(_dataAppointment.ElementType);
            mockAppointmentSet.As<IQueryable<Appointment>>().Setup(x => x.GetEnumerator())
                .Returns(_dataAppointment.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.BreakTimes).Returns(mockBreakTimeSet.Object);
            mockContext.Setup(x => x.Schedules).Returns(mockScheduleSet.Object);
            mockContext.Setup(x => x.Appointments).Returns(mockAppointmentSet.Object);

            _model = new(mockContext.Object);
        }

        private readonly Appointment _input_0 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void AddValidAppointment()
        {
            _model.Add(_input_0);
            Assert.IsTrue(true);
        }

        private readonly Appointment _input_1 = new()
        {
            Id = 2L,
            ScheduleId = 2L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void AddInvalidAppointment_0()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Add(_input_1),
                "Invalid referred schedule");
        }

        private readonly Appointment _input_2 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 20, 45, 0),
            End = new DateTime(2023, 1, 2, 21, 15, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void AddInvalidAppointment_1()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Add(_input_2),
                "Appointment is not within its referred schedule");
        }

        private readonly Appointment _input_3 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 11, 45, 0),
            End = new DateTime(2023, 1, 2, 12, 15, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void AddInvalidAppointment_2()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Add(_input_3),
                "Appointment overlaps breakTimes");
        }

        private readonly Appointment _input_4 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 5, 0),
            End = new DateTime(2023, 1, 2, 9, 9, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void AddInvalidAppointment_3()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Add(_input_4),
                "Appointment overlaps other appointments");
        }

        private readonly Appointment _input_5 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 0, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void UpdateValidAppointment_0()
        {
            _model.Update(_input_5);
            Assert.IsTrue(true);
        }

        private readonly Appointment _input_6 = new()
        {
            Id = 1L,
            ScheduleId = 2L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void UpdateInvalidAppointment_0()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Update(_input_6),
                "Invalid referred schedule");
        }

        private readonly Appointment _input_7 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 20, 45, 0),
            End = new DateTime(2023, 1, 2, 21, 15, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void UpdateInvalidAppointment_1()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Update(_input_7),
                "Appointment is not within its referred schedule");
        }

        private readonly Appointment _input_8 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 11, 45, 0),
            End = new DateTime(2023, 1, 2, 12, 15, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void UpdateInvalidAppointment_2()
        {
            Assert.ThrowsException<ServiceException>(() => _model.Update(_input_8),
                "Appointment overlaps breakTimes");
        }

        private readonly Appointment _input_9 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 5, 0),
            End = new DateTime(2023, 1, 2, 9, 9, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public void UpdateValidAppointment_1()
        {
            _model.Update(_input_9);
            Assert.IsTrue(true);
        }
    }
}
