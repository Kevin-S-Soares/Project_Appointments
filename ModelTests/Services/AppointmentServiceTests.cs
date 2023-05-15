using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services;
using Project_Appointments.Services.AppointmentService;
using Project_Appointments.Services.AuthService;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.ScheduleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTests.Services
{
    [TestClass]
    public class AppointmentServiceTests
    {
        private AppointmentService _model = default!;
        private Mock<IScheduleService> _mockScheduleService = default!;
        private Mock<IAuthService> _authMock = default!;

        private readonly BreakTime _dataBreakTime = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(12, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(13, 0, 0)
        };

        private readonly Schedule _dataSchedule = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(21, 0, 0),
        };


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
            _mockScheduleService = new Mock<IScheduleService>();
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>())).Returns(
                new ServiceResponse<Schedule>(_dataSchedule, 200));

            var mockBreakTimeService = new Mock<IBreakTimeService>();
            mockBreakTimeService.Setup(x => x.FindAllFromSameSchedule(It.IsAny<Appointment>())).Returns(
                new ServiceResponse<IEnumerable<BreakTime>>(
                    new List<BreakTime>() { _dataBreakTime }, 200));

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
            mockContext.Setup(x => x.Appointments).Returns(mockAppointmentSet.Object);

            _authMock = new();
            _authMock.Setup(x => x.IsAdmin()).Returns(true);
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(false);
            _authMock.Setup(x => x.IsAttendant()).Returns(false);

            _model = new(mockContext.Object, mockBreakTimeService.Object,
                _mockScheduleService.Object, _authMock.Object);
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
            var result = _model.Create(_input_0);
            Assert.AreEqual(expected: _input_0, actual: result.Data);
            Assert.AreEqual(expected: 201, actual: result.StatusCode);
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
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>())).Returns(
                new ServiceResponse<Schedule>("Invalid referred schedule", 500));

            var result = _model.Create(_input_1);
            Assert.AreEqual(expected: "Invalid referred schedule", actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Create(_input_2);
            Assert.AreEqual(expected: "Appointment is not within its referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Create(_input_3);
            Assert.AreEqual(expected: "Appointment overlaps breakTimes",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Create(_input_4);
            Assert.AreEqual(expected: "Appointment overlaps other appointments",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Update(_input_5);
            Assert.AreEqual(expected: _input_5, actual: result.Data);
            Assert.AreEqual(expected: 200, actual: result.StatusCode);
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
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>())).Returns(
                new ServiceResponse<Schedule>("Invalid referred schedule", 500));

            var result = _model.Update(_input_6);
            Assert.AreEqual(expected: "Invalid referred schedule", actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Update(_input_7);
            Assert.AreEqual(expected: "Appointment is not within its referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Update(_input_8);
            Assert.AreEqual(expected: "Appointment overlaps breakTimes",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: 500, actual: result.StatusCode);
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
            var result = _model.Update(_input_9);
            Assert.AreEqual(expected: _input_9, actual: result.Data);
            Assert.AreEqual(expected: 200, actual: result.StatusCode);
        }

        private readonly Appointment _input_10 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        private readonly Appointment _input_11 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 0, 0),
            End = new DateTime(2023, 1, 2, 9, 14, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public async Task AdminAuthorized()
        {
            var result1 = await _model.CreateAsync(_input_10);
            var result2 = _model.FindById(_input_11.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_11);
            var result5 = await _model.DeleteAsync(_input_11.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }

        private readonly Appointment _input_12 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        private readonly Appointment _input_13 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 0, 0),
            End = new DateTime(2023, 1, 2, 9, 14, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public async Task OdontologistAuthorized()
        {
            _authMock.Setup(x => x.IsAdmin()).Returns(false);
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(true);

            var result1 = await _model.CreateAsync(_input_12);
            var result2 = _model.FindById(_input_13.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_13);
            var result5 = await _model.DeleteAsync(_input_13.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }

        private readonly Appointment _input_14 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 15, 0),
            End = new DateTime(2023, 1, 2, 9, 29, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        private readonly Appointment _input_15 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 2, 9, 0, 0),
            End = new DateTime(2023, 1, 2, 9, 14, 0),
            PatientName = "Test test",
            Description = "I am testing"
        };

        [TestMethod]
        public async Task AttendantAuthorized()
        {
            _authMock.Setup(x => x.IsAdmin()).Returns(false);
            _authMock.Setup(x => x.IsAttendant()).Returns(true);

            var result1 = await _model.CreateAsync(_input_14);
            var result2 = _model.FindById(_input_15.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_15);
            var result5 = await _model.DeleteAsync(_input_15.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }
    }
}
