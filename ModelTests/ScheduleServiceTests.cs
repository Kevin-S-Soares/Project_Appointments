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

namespace ModelTests
{
    [TestClass]
    public class ScheduleServiceTests
    {
        private ScheduleService _model = default!;

        private readonly IQueryable<Schedule> _data = new List<Schedule>()
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

        [TestInitialize]
        public void Setup()
        {
            var mockSet = new Mock<DbSet<Schedule>>();
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.Provider).Returns(_data.Provider);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.Expression).Returns(_data.Expression);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.ElementType).Returns(_data.ElementType);
            mockSet.As<IQueryable<Schedule>>().Setup(x => x.GetEnumerator()).Returns(_data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.Schedules).Returns(mockSet.Object);

            _model = new(mockContext.Object);
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
        public void AddValidSchedule()
        {
            _model.Add(_input_0);
            Assert.IsTrue(true);
        }

        private readonly Schedule _input_1 = new()
        {
            Id = 3L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void AddInvalidSchedule()
        {
            Assert.ThrowsException<ServiceException>(
                () => _model.Add(_input_1), "Schedule overlaps other schedules");
        }

        private readonly Schedule _input_2 = new()
        {
            Id = 2L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Wednesday,
            StartTime = new TimeSpan(9, 0, 0),
            EndDay = DayOfWeek.Wednesday,
            EndTime = new TimeSpan(18, 0, 0),
        };

        [TestMethod]
        public void UpdateValidSchedule()
        {
            _model.Update(_input_2);
            Assert.IsTrue(true);
        }

        private readonly Schedule _input_3 = new()
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
            Assert.ThrowsException<ServiceException>(
                () => _model.Update(_input_3), "Schedule overlaps other schedules");
        }


    }
}
