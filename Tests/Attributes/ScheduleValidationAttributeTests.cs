using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;
using Server.Models.Attributes;
using System;

namespace ModelTests
{
    [TestClass]
    public class ScheduleValidationAttributeTests
    {
        private ScheduleValidationAttribute _validation = default!;

        [TestInitialize]
        public void Setup()
        {
            _validation = new();
        }

        private readonly Schedule _input_0 = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            EndDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        private readonly bool _expectedResult_0 = true;

        [TestMethod]
        public void ValidSchedule()
        {
            bool actualResult = _validation.IsValid(_input_0);
            Assert.AreEqual(expected: _expectedResult_0, actual: actualResult);
        }

        private readonly object? _input_1 = null;

        private readonly string _expectedResult_1 = "Invalid Schedule";

        [TestMethod]
        public void InvalidSchedule()
        {
            _validation.IsValid(_input_1);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_1, actual: actualResult);
        }

        private readonly Schedule _input_2 = new()
        {
            Id = -1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            EndDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        private readonly string _expectedResult_2 = "Invalid Schedule.Id";

        [TestMethod]
        public void InvalidId()
        {
            _validation.IsValid(_input_2);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_2, actual: actualResult);
        }

        private readonly Schedule _input_3 = new()
        {
            Id = 1L,
            OdontologistId = -1L,
            StartDay = DayOfWeek.Monday,
            EndDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        private readonly string _expectedResult_3 = "Invalid Schedule.OdontologistId";

        [TestMethod]
        public void InvalidScheduleId()
        {
            _validation.IsValid(_input_3);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_3, actual: actualResult);
        }

        private readonly Schedule _input_4 = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = (DayOfWeek)(-1),
            EndDay = DayOfWeek.Monday,
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        private readonly string _expectedResult_4 = "Invalid Schedule.StartDay";

        [TestMethod]
        public void InvalidStartDay()
        {
            _validation.IsValid(_input_4);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_4, actual: actualResult);
        }

        private readonly Schedule _input_5 = new()
        {
            Id = 1L,
            OdontologistId = 1L,
            StartDay = DayOfWeek.Monday,
            EndDay = (DayOfWeek)(7),
            StartTime = new TimeSpan(9, 0, 0),
            EndTime = new TimeSpan(18, 0, 0)
        };

        private readonly string _expectedResult_5 = "Invalid Schedule.EndDay";

        [TestMethod]
        public void InvalidEndDay()
        {
            _validation.IsValid(_input_5);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_5, actual: actualResult);
        }
    }
}
