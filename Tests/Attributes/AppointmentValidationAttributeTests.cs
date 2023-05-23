using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server.Models;
using Server.Models.Attributes;
using System;

namespace ModelTests
{
    [TestClass]
    public class AppointmentValidationAttributeTests
    {
        private AppointmentValidationAttribute _validation = default!;

        [TestInitialize]
        public void Setup()
        {
            _validation = new();
        }

        private readonly Appointment _input_0 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 1, 9, 0, 0),
            End = new DateTime(2023, 1, 1, 9, 15, 0),
            PatientName = "Mario Bros",
            Description = "Cleaning"
        };

        private readonly bool _expectedResult_0 = true;

        [TestMethod]
        public void ValidAppointment()
        {
            bool actualResult = _validation.IsValid(_input_0);
            Assert.AreEqual(expected: _expectedResult_0, actual: actualResult);
        }

        private readonly object? _input_1 = null;

        private readonly string _expectedResult_1 = "Invalid Appointment";

        [TestMethod]
        public void InvalidAppointment()
        {
            _validation.IsValid(_input_1);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_1, actual: actualResult);
        }

        private readonly Appointment _input_2 = new()
        {
            Id = -1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 1, 9, 0, 0),
            End = new DateTime(2023, 1, 1, 9, 15, 0),
            PatientName = "Mario Bros",
            Description = "Cleaning"
        };

        private readonly string _expectedResult_2 = "Invalid Appointment.Id";

        [TestMethod]
        public void InvalidId()
        {
            _validation.IsValid(_input_2);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_2, actual: actualResult);
        }

        private readonly Appointment _input_3 = new()
        {
            Id = 1L,
            ScheduleId = -1L,
            Start = new DateTime(2023, 1, 1, 9, 0, 0),
            End = new DateTime(2023, 1, 1, 9, 15, 0),
            PatientName = "Mario Bros",
            Description = "Cleaning"
        };

        private readonly string _expectedResult_3 = "Invalid Appointment.ScheduleId";

        [TestMethod]
        public void InvalidScheduleId()
        {
            _validation.IsValid(_input_3);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_3, actual: actualResult);
        }
        private readonly Appointment _input_4 = new()
        {
            Id = 1L,
            ScheduleId = 1L,
            Start = new DateTime(2023, 1, 1, 9, 0, 0),
            End = new DateTime(2023, 1, 1, 9, 15, 0),
            PatientName = "MarioBros",
            Description = "Cleaning"
        };

        private readonly string _expectedResult_4 = "Invalid Appointment.PatientName";

        [TestMethod]
        public void InvalidName()
        {
            _validation.IsValid(_input_4);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_4, actual: actualResult);
        }
    }
}
