using Moq;
using NUnit.Framework;
using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices;
using EvidentaStudentilor.RepositoryInterfaces;
using EvidentaStudentilor.Repositories;
using EvidentaStudentilor.LogicServices.ServiceInterfaces;

namespace EvidentaStudentilor.Tests.ServiceTests
{
    internal class DepartmentServiceTests
    {
        private Mock<IWrapperRepository> mockWrapperRepository;


        [OneTimeSetUp]
        public void Init()
        {
            mockWrapperRepository = new Mock<IWrapperRepository>();
            mockWrapperRepository.Setup(x => x.DepartmentRepository.FindAll()).Returns(GetFakeDepartmentsList());
        }


        private List<Department> GetFakeDepartmentsList()
        {
            return new List<Department>() {
                        new Department {Id = 1, Name = "DCTI"},
                        new Department {Id = 2, Name = "DAE" }
            };
        }


        [Test]
        public void HavingDepartmentService_WhenGetDepartment_ThenReturnAllDeparments()
        {
            //Arange
            DepartmentService departmentService = new DepartmentService(mockWrapperRepository.Object);

            //Act
            var result = departmentService.GetDepartment();

            //Result
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void HavingDepartmentService_WhenCreateUpdateAndDeleteDepartment_ThenReturnOK()
        {
            //Arange
            DepartmentService departmentService = new DepartmentService(mockWrapperRepository.Object);

            //Act
            departmentService.createDepartment(new Department { Id = 3, Name = "DMRR" });
            departmentService.updateDepartment(new Department { Id = 3, Name = "DMR" });
            departmentService.deleteDepartment(new Department { Id = 3, Name = "DMR" });
            var result = departmentService.GetDepartment();

            //Result
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void HavingDepartmentService_WhenGetDepartmentId_ThenReturnAskedDeparment()
        {
            //Arange
            DepartmentService departmentService = new DepartmentService(mockWrapperRepository.Object);

            //Act
            var result = departmentService.GetDepartment(1);

            //Result
            Assert.That(result.Name, Is.EqualTo("DCTI"));
        }
    }
}
