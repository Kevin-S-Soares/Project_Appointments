﻿using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.Exceptions;
using Project_Appointments.Services.ScheduleService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests
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

        private readonly IQueryable<Odontologist> _dataOdontologist = new List<Odontologist>()
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
        }.AsQueryable();

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

            var mockOdontologist = new Mock<DbSet<Odontologist>>();
            mockOdontologist.As<IQueryable<Odontologist>>().Setup(x => x.Provider)
                .Returns(_dataOdontologist.Provider);
            mockOdontologist.As<IQueryable<Odontologist>>().Setup(x => x.Expression)
                .Returns(_dataOdontologist.Expression);
            mockOdontologist.As<IQueryable<Odontologist>>().Setup(x => x.ElementType)
                .Returns(_dataOdontologist.ElementType);
            mockOdontologist.As<IQueryable<Odontologist>>().Setup(x => x.GetEnumerator())
                .Returns(_dataOdontologist.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.Schedules).Returns(mockSet.Object);
            mockContext.Setup(x => x.Odontologists).Returns(mockOdontologist.Object);

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
        public void AddValidSchedule_0()
        {
            _model.Add(_input_0);
            Assert.IsTrue(true);
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
        public void AddValidSchedule_1()
        {
            _model.Add(_input_1);
            Assert.IsTrue(true);
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
        public void AddInvalidSchedule_0()
        {
            Assert.ThrowsException<ServiceException>(
                () => _model.Add(_input_2), "Schedule overlaps other schedules");
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
            _model.Update(_input_3);
            Assert.IsTrue(true);
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
            _model.Update(_input_4);
            Assert.IsTrue(true);
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
            Assert.ThrowsException<ServiceException>(
                () => _model.Update(_input_5), "Schedule overlaps other schedules");
        }

        private readonly Schedule _input_6 = new()
        {
            Id = 3L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(8, 0, 0),
            EndDay = DayOfWeek.Monday,
            EndTime = new TimeSpan(19, 0, 0),
        };

        [TestMethod]
        public void AddInvalidSchedule_1()
        {
            Assert.ThrowsException<ServiceException>(
                () => _model.Add(_input_6), "Schedule overlaps other schedules");
        }

    }
}
