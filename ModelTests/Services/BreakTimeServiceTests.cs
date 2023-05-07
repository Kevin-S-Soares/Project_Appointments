using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services;
using Project_Appointments.Services.BreakTimeService;
using Project_Appointments.Services.ScheduleService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests
{
    [TestClass]
    public class BreakTimeServiceTests
    {
        private BreakTimeService _model = default!;
        private Mock<IScheduleService> _mockScheduleService = default!;

        private readonly Schedule _schedule = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(21, 0, 0)
        };

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

        private readonly IQueryable<Schedule> _dataSchedule = new List<Schedule>()
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


            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.BreakTimes).Returns(mockBreakTimeSet.Object);
            mockContext.Setup(x => x.Schedules).Returns(mockScheduleSet.Object);

            _mockScheduleService = new Mock<IScheduleService>();
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>()))
                .Returns(new ServiceResponse<Schedule>(_schedule, 200));

            _model = new(mockContext.Object, _mockScheduleService.Object);
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
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>()))
                .Returns(new ServiceResponse<Schedule>("BreakTime does not exist", StatusCodes.Status404NotFound));

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
            _mockScheduleService.Setup(x => x.FindById(It.IsAny<long>()))
                .Returns(new ServiceResponse<Schedule>("BreakTime does not exist", StatusCodes.Status404NotFound));

            var result = _model.Update(_input_5);
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
    }
}
