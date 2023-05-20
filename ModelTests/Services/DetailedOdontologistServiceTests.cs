using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModelTests.Tools;
using Moq;
using Project_Appointments.Contexts;
using Project_Appointments.Models;
using Project_Appointments.Services.AuthService;
using Project_Appointments.Services.DetailedOdontologistService;
using System.Collections.Generic;
using System.Linq;

namespace ModelTests.Services
{
    [TestClass]
    public class DetailedOdontologistServiceTests
    {
        private DetailedOdontologistService _model = default!;
        private Mock<IAuthService> _mockAuthService = default!;

        private readonly IQueryable<Odontologist> _odontologists = new List<Odontologist>()
        {
            new()
            {
                Id = 1L,
                Name = "Test Test",
                Email = "test@test.com",
                Phone = "(011) 91111-1111"
            }
        }.AsQueryable();

        private readonly IQueryable<Schedule> _schedules = new List<Schedule>()
        {

        }.AsQueryable();

        private readonly IQueryable<Appointment> _appointments = new List<Appointment>()
        {

        }.AsQueryable();

        private readonly IQueryable<BreakTime> _breakTimes = new List<BreakTime>()
        {

        }.AsQueryable();

        [TestInitialize]
        public void Setup()
        {
            var mockOdontologistSet = new Mock<DbSet<Odontologist>>();
            var mockScheduleSet = new Mock<DbSet<Schedule>>();
            var mockAppointmentSet = new Mock<DbSet<Appointment>>();
            var mockBreakTimeSet = new Mock<DbSet<BreakTime>>();

            mockOdontologistSet.BindData(_odontologists);
            mockScheduleSet.BindData(_schedules);
            mockAppointmentSet.BindData(_appointments);
            mockBreakTimeSet.BindData(_breakTimes);

            var mockContext = new Mock<ApplicationContext>();
            mockContext.Setup(x => x.Odontologists).Returns(mockOdontologistSet.Object);
            mockContext.Setup(x => x.Schedules).Returns(mockScheduleSet.Object);
            mockContext.Setup(x => x.Appointments).Returns(mockAppointmentSet.Object);
            mockContext.Setup(x => x.BreakTimes).Returns(mockBreakTimeSet.Object);

            _mockAuthService = new();
            _mockAuthService.Setup(x => x.IsAdmin()).Returns(false);
            _mockAuthService.Setup(x => x.IsAttendant()).Returns(false);
            _mockAuthService.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(false);

            _model = new(mockContext.Object, _mockAuthService.Object);
        }

        [TestMethod]
        public void AdminAuthorized()
        {
            _mockAuthService.Setup(x => x.IsAdmin()).Returns(true);

            var result1 = _model.FindById(1);
            var result2 = _model.FindAll();

            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
        }

        [TestMethod]
        public void OdontologistAuthorized()
        {
            _mockAuthService.Setup(x => x.IsOdontologist(It.IsAny<long>())).Returns(true);

            var result1 = _model.FindById(1);
            var result2 = _model.FindAll();

            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status403Forbidden, actual: result2.StatusCode);
        }

        [TestMethod]
        public void AttendantAuthorized()
        {
            _mockAuthService.Setup(x => x.IsAttendant()).Returns(true);

            var result1 = _model.FindById(1);
            var result2 = _model.FindAll();

            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result1.StatusCode);
            Assert.AreEqual(expected: StatusCodes.Status200OK, actual: result2.StatusCode);
        }
    }
}
