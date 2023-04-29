using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services;
using Project_Appointments.Services.OdontologistService;
using Project_Appointments.Services.ScheduleService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests.Services
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private ScheduleService _model = default!;

        private readonly IQueryable<Schedule> _dataSchedule = new List<Schedule>()
        {
            new()
            {
                Id = 1L,
                OdontologistId = 1L,
                StartDay = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndDay = DayOfWeek.Monday,
                EndTime = new TimeSpan(18, 0, 0),
            },
            new()
            {
                Id = 2L,
                OdontologistId = 1L,
                StartDay = DayOfWeek.Tuesday,
                StartTime = new TimeSpan(9, 0, 0),
                EndDay = DayOfWeek.Tuesday,
                EndTime = new TimeSpan(18, 0, 0),
            }
        }.AsQueryable();

        private readonly List<Odontologist> _dataOdontologist = new()
        {
            new()
            {
                Id = 1L,
                Name = "Test Test",
                Email = "test@test.com",
                Phone = "(011) 91111-1111"
            },
            new()
            {
                Id = 2L,
                Name = "Test Test",
                Email = "test@test.com",
                Phone = "(011) 91111-1111"
            }
        };

        [TestInitialize]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Schedule>>();
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.Provider)
                .Returns(_dataSchedule.Provider);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.Expression)
                .Returns(_dataSchedule.Expression);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.ElementType)
                .Returns(_dataSchedule.ElementType);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.GetEnumerator())
                .Returns(_dataSchedule.GetEnumerator());

            var mockOdontologist = new Mock<IOdontologistService>();
            mockOdontologist.Setup(x => x.FindById(It.IsAny<long>())).
                Returns(new ServiceResponse<Odontologist>(_dataOdontologist[0], 200));

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.Schedules).Returns(mockSet.Object);
            _model = new(mockContext.Object, mockOdontologist.Object);
        }

        private readonly Schedule _input_0 = new()
        {
            Id = 3L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Wednesday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Wednesday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void CreateValidSchedule_0()
        {
            var result = _model.Create(_input_0);
            Assert.AreEqual(expected: StatusCodes.Status201Created,
                actual: result.StatusCode);
        }

        private readonly Schedule _input_1 = new()
        {
            Id = 3L,
            OdontologistId = 2L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void CreateValidSchedule_1()
        {
            var result = _model.Create(_input_1);
            Assert.AreEqual(expected: StatusCodes.Status201Created,
                actual: result.StatusCode);
        }

        private readonly Schedule _input_2 = new()
        {
            Id = 3L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void CreateInvalidSchedule_0()
        {
            var result = _model.Create(_input_5);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
            Assert.AreEqual(expected: "Schedule overlaps other schedules",
                actual: result.ErrorMessage);
        }

        private readonly Schedule _input_3 = new()
        {
            Id = 2L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Wednesday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Wednesday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void UpdateValidSchedule_0()
        {
            var result = _model.Update(_input_3);
            Assert.AreEqual(expected: StatusCodes.Status200OK,
                actual: result.StatusCode);
        }

        private readonly Schedule _input_4 = new()
        {
            Id = 2L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Tuesday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Tuesday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void UpdateValidSchedule_1()
        {
            var result = _model.Update(_input_4);
            Assert.AreEqual(expected: StatusCodes.Status200OK,
                actual: result.StatusCode);
        }

        private readonly Schedule _input_5 = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Tuesday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Tuesday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void UpdateInvalidSchedule()
        {
            var result = _model.Update(_input_5);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
            Assert.AreEqual(expected: "Schedule overlaps other schedules",
                actual: result.ErrorMessage);
        }

        private readonly Schedule _input_6 = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(8, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(19, 0, 0),
        };

        [TestMethod]
        public void AddInvalidSchedule_1()
        {
            var result = _model.Update(_input_5);
            Assert.AreEqual(expected: StatusCodes.Status500InternalServerError,
                actual: result.StatusCode);
            Assert.AreEqual(expected: "Schedule overlaps other schedules",
                actual: result.ErrorMessage);
        }

    }
}
