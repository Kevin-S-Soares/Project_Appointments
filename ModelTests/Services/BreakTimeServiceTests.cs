using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTests.Tools;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.ContextModels;
using Project_Appointments.Services.AuthService;
using Project_Appointments.Services.BreakTimeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTests.Services
{
    [TestClass]
    public class BreakTimeServiceTests
    {
        private BreakTimeService _model = default!;
        private Mock<IAuthService> _authMock = default!;

        private readonly IQueryable<ContextBreakTime> _dataBreakTime = new List<ContextBreakTime>()
        {
            new()
            {
                Id = 1L,
                ScheduleId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(12, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(13, 0, 0)
            },
            new()
            {
                Id = 2L,
                ScheduleId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(14, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(15, 0, 0)
            }
        }.AsQueryable();

        private readonly IQueryable<ContextSchedule> _dataSchedule = new List<ContextSchedule>()
        {
            new()
            {
                Id = 1L,
                OdontologistId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(21, 0, 0)
            }

        }.AsQueryable();

        [TestInitialize]
        public void Setup()
        {
            var mockBreakTimeSet = new Mock<DbSet<ContextBreakTime>>();
            mockBreakTimeSet.BindData(_dataBreakTime);

            var mockScheduleSet = new Mock<DbSet<ContextSchedule>>();
            mockScheduleSet.BindData(_dataSchedule);

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.BreakTimes).Returns(mockBreakTimeSet.Object);
            mockContext.Setup(x => x.Schedules).Returns(mockScheduleSet.Object);


            _authMock = new();
            _authMock.Setup(x => x.IsAdmin()).Returns(true);
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(false);
            _authMock.Setup(x => x.IsAttendant()).Returns(false);

            _model = new(mockContext.Object, _authMock.Object);
        }

        private readonly BreakTime _input_0 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(17, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0)
        };

        [TestMethod]
        public void AddValidBreakTime()
        {
            var result = _model.Create(_input_0);
            Assert.AreEqual(expected: _input_0,
                actual: result.Data);
            Assert.AreEqual(expected: StatusCodes.Status201Created,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_1 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(11, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(14, 0, 0)
        };

        [TestMethod]
        public void AddInvalidBreakTime_0()
        {
            var result = _model.Create(_input_1);
            Assert.AreEqual(expected: "BreakTime overlaps other breakTimes",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_2 = new()
        {
            Id = 3L,
            ScheduleId = 2L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0)
        };

        [TestMethod]
        public void AddInvalidBreakTime_1()
        {
            var result = _model.Create(_input_2);
            Assert.AreEqual(expected: "Invalid referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_3 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(7, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(8, 0, 0)
        };

        [TestMethod]
        public void AddInvalidBreakTime_2()
        {
            var result = _model.Create(_input_3);
            Assert.AreEqual(expected: "BreakTime is not within its referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }


        private readonly BreakTime _input_4 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(12, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(13, 0, 0)
        };

        [TestMethod]
        public void UpdateValidBreakTime()
        {
            var result = _model.Update(_input_4);
            Assert.AreEqual(expected: _input_4,
                actual: result.Data);
            Assert.AreEqual(expected: StatusCodes.Status200OK,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_5 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(11, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(14, 0, 0)
        };

        [TestMethod]
        public void UpdateInvalidBreakTime_0()
        {
            var result = _model.Update(_input_5);
            Assert.AreEqual(expected: "BreakTime overlaps other breakTimes",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_6 = new()
        {
            Id = 2L,
            ScheduleId = 2L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0)
        };

        [TestMethod]
        public void UpdateInvalidBreakTime_1()
        {
            var result = _model.Update(_input_6);
            Assert.AreEqual(expected: "Invalid referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_7 = new()
        {
            Id = 2L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(7, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(8, 0, 0)
        };

        [TestMethod]
        public void UpdateInvalidBreakTime_2()
        {
            var result = _model.Update(_input_7);
            Assert.AreEqual(expected: "BreakTime is not within its referred schedule",
                actual: result.ErrorMessage);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
        }

        private readonly BreakTime _input_8 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(16, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(17, 0, 0)
        };

        private readonly BreakTime _input_9 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(12, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(13, 0, 0)
        };

        [TestMethod]
        public async Task AdminAuthorized()
        {
            var result1 = await _model.CreateAsync(_input_8);
            var result2 = _model.FindById(_input_9.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_9);
            var result5 = await _model.DeleteAsync(_input_9.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }

        private readonly BreakTime _input_10 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(16, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(17, 0, 0)
        };

        private readonly BreakTime _input_11 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(12, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(13, 0, 0)
        };

        [TestMethod]
        public async Task OdontologistAuthorized()
        {
            _authMock.Setup(x => x.IsAdmin()).Returns(false);
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(true);

            var result1 = await _model.CreateAsync(_input_10);
            var result2 = _model.FindById(_input_11.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_11);
            var result5 = await _model.DeleteAsync(_input_11.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }

        private readonly BreakTime _input_12 = new()
        {
            Id = 3L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(16, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(17, 0, 0)
        };

        private readonly BreakTime _input_13 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(12, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(13, 0, 0)
        };

        [TestMethod]
        public async Task AttendantAuthorized()
        {
            _authMock.Setup(x => x.IsAdmin()).Returns(false);
            _authMock.Setup(x => x.IsAttendant()).Returns(true);

            var result1 = await _model.CreateAsync(_input_12);
            var result2 = _model.FindById(_input_13.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_13);
            var result5 = await _model.DeleteAsync(_input_13.Id);

            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result5.StatusCode);
        }
    }
}
