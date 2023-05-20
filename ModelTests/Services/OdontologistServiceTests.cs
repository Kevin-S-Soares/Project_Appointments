using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTests.Tools;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Models.ContextModels;
using Project_Appointments.Services.AuthService;
using Project_Appointments.Services.OdontologistService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelTests.Services
{
    [TestClass]
    public class OdontologistServiceTests
    {

        private OdontologistService _model = default!;
        private Mock<IAuthService> _authMock = default!;
        private readonly IQueryable<ContextOdontologist> _dataOdontologists = new List<ContextOdontologist>()
        {
            new()
            {
                Id = 0,
                Email = "Test@Test.com",
                Phone = "(011) 91111-1111",
                Name = "Test Test",
            }
        }.AsQueryable();

        [TestInitialize]
        public void Setup()
        {
            var contextMock = new Mock<ApplicationContext>();
            var _mockOdontologistSet = new Mock<DbSet<ContextOdontologist>>();
            _mockOdontologistSet.BindData(_dataOdontologists);
            contextMock.Setup(x => x.Odontologists).Returns(_mockOdontologistSet.Object);
            _authMock = new();

            _authMock.Setup(x => x.IsAdmin()).Returns(false);
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(false);
            _authMock.Setup(x => x.IsAttendant()).Returns(false);
            _model = new(contextMock.Object, _authMock.Object);
        }

        private readonly Odontologist _input_0 = new()
        {
            Id = 0,
            Email = "Test@Test.com",
            Phone = "(011) 91111-1111",
            Name = "Test Test",
        };

        [TestMethod]
        public async Task AdminAuthorized()
        {
            _authMock.Setup(x => x.IsAdmin()).Returns(true);

            var result1 = await _model.CreateAsync(_input_0);
            var result2 = _model.FindById(_input_0.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_0);
            var result5 = await _model.DeleteAsync(_input_0.Id);

            Assert.AreEqual(expected: StatusCodes.Status201Created, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result5.StatusCode);
        }

        private readonly Odontologist _input_1 = new()
        {
            Id = 0,
            Email = "Test@Test.com",
            Phone = "(011) 91111-1111",
            Name = "Test Test",
        };

        [TestMethod]
        public async Task OdontologistAuthorized()
        {
            _authMock.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(true);

            var result1 = await _model.CreateAsync(_input_1);
            var result2 = _model.FindById(_input_1.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_1);
            var result5 = await _model.DeleteAsync(_input_1.Id);

            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result5.StatusCode);
        }

        private readonly Odontologist _input_2 = new()
        {
            Id = 0,
            Email = "Test@Test.com",
            Phone = "(011) 91111-1111",
            Name = "Test Test",
        };

        [TestMethod]
        public async Task AttendantAuthorized()
        {
            _authMock.Setup(x => x.IsAttendant()).Returns(true);

            var result1 = await _model.CreateAsync(_input_2);
            var result2 = _model.FindById(_input_2.Id);
            var result3 = _model.FindAll();
            var result4 = await _model.UpdateAsync(_input_2);
            var result5 = await _model.DeleteAsync(_input_2.Id);

            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result3.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result4.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result5.StatusCode);
        }
    }
}
