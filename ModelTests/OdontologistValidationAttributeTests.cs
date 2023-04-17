using Appointments_Project.Models.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Project_Appointments.Models;

namespace ModelTests
{
    [TestClass]
    public class OdontologistValidationAttributeTests
    {
        private OdontologistValidationAttribute _validation = default!;

        [TestInitialize]
        public void Setup()
        {
            _validation = new();
        }

        private readonly Odontologist _input_0 = new()
        {
            Id = 1L,
            Name = "Mario Bros",
            Email = "mario@bros.com",
            Phone = "(011) 91111-1111"
        };
        private readonly bool _expectedResult_0 = true;

        [TestMethod]
        public void ValidOdontologist()
        {
            bool actualResult = _validation.IsValid(_input_0);
            Assert.AreEqual(expected: _expectedResult_0, actual: actualResult);
        }

        private readonly object? _input_1 = null;
        private readonly string _expectedResult_1 = "Invalid Odontologist";

        [TestMethod]
        public void InvalidOdontologist()
        {
            _validation.IsValid(_input_1);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_1, actual: actualResult);
        }

        private readonly Odontologist _input_2 = new()
        {
            Id = -1L,
            Name = "Mario Bros",
            Email = "mario@bros.com",
            Phone = "(011) 91111-1111"
        };
        private readonly string _expectedResult_2 = "Invalid Odontologist.Id";

        [TestMethod]
        public void InvalidOdontologistId()
        {
            _validation.IsValid(_input_2);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_2, actual: actualResult);
        }
        private readonly Odontologist _input_3 = new()
        {
            Id = 1L,
            Name = "MarioBros",
            Email = "mario@bros.com",
            Phone = "(011) 91111-1111"
        };
        private readonly string _expectedResult_3 = "Invalid Odontologist.Name";

        [TestMethod]
        public void InvalidOdontologistName()
        {
            _validation.IsValid(_input_3);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_3, actual: actualResult);
        }
        private readonly Odontologist _input_4 = new()
        {
            Id = 1L,
            Name = "Mario Bros",
            Email = "mario.bros.com",
            Phone = "(011) 91111-1111"
        };
        private readonly string _expectedResult_4 = "Invalid Odontologist.Email";

        [TestMethod]
        public void InvalidOdontologistEmail()
        {
            _validation.IsValid(_input_4);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_4, actual: actualResult);
        }

        private readonly Odontologist _input_5 = new()
        {
            Id = 1L,
            Name = "Mario Bros",
            Email = "mario@bros.com",
            Phone = "(011)91111-1111"
        };
        private readonly string _expectedResult_5 = "Invalid Odontologist.Phone";

        [TestMethod]
        public void InvalidOdontologistPhone()
        {
            _validation.IsValid(_input_5);
            string? actualResult = _validation.ErrorMessage;
            Assert.AreEqual(expected: _expectedResult_5, actual: actualResult);
        }
    }
}