using EvidentaStudentilor.DataModel;
using EvidentaStudentilor.LogicServices;
using EvidentaStudentilor.RepositoryInterfaces;
using Moq;
using NUnit.Framework;

namespace EvidentaStudentilor.Tests.ServiceTests
{
    internal class UserServiceTests
    {
        private Mock<IWrapperRepository> mockWrapperRepository;


        [OneTimeSetUp]
        public void Init()
        {
            mockWrapperRepository = new Mock<IWrapperRepository>();
            mockWrapperRepository.Setup(x => x.UserRepository.FindAll()).Returns(GetFakeUsersList());
            mockWrapperRepository.Setup(x => x.RoleRepository.FindAll()).Returns(GetFakeRolesList());
        }


        private List<User> GetFakeUsersList()
        {
            return new List<User>() {
                        new User {Id = 1, RoleId = 1, Email = "livia@livia.com", Paswword = "livia"},
                        new User {Id = 2, RoleId = 1, Email = "livia.zane@gmail.com", Paswword = "livia"},
                        new User {Id = 3, RoleId = 2, Email = "secretar@secretar.com", Paswword = "secretar"}
            };
        }

        private List<Role> GetFakeRolesList()
        {
            return new List<Role>() {
                        new Role {Id = 1, Name = "Student"},
                        new Role {Id = 2, Name = "Secretary"}
            };
        }


        [Test]
        public void HavingUserService_WhenGetUsers_ThenReturnAllUsers()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUser();

            //Result
            Assert.That(result.Count(), Is.EqualTo(3));
        }


        [Test]
        public void HavingUserService_WhenGetUserId_ThenReturnAskedUser()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUser(1);

            //Result
            Assert.That(result.Email, Is.EqualTo("livia@livia.com"));
            Assert.That(result.Paswword, Is.EqualTo("livia"));
        }


        [Test]
        public void HavingUserService_WhenGetUserByEmailAndPassword_ThenReturnAskedUser()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUserByEmailPassword("livia.zane@gmail.com", "livia");


            //Result
            Assert.That(result.Id, Is.EqualTo(2));
        }


        [Test]
        public void HavingUserService_WhenGetUserRole_ThenReturnRoleName()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUserRole(3);

            //Result
            Assert.That(result, Is.EqualTo("Secretary"));
        }


        [Test]
        public void HavingUserService_WhenGetUsersWithNamedRole_ThenReturnAllUsersWithThatRole()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUsersRole("Student");

            //Result
            Assert.That(result.Count(), Is.EqualTo(2));
        }


        [Test]
        public void HavingUserService_WhenGetUserByEmail_ThenReturnThatUser()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUserByEmail("secretar@secretar.com");

            //Result
            Assert.That(result.Id, Is.EqualTo(3));
        }


        [Test]
        public void HavingUserService_WhenGetUserIdByEmail_ThenReturnThatUserId()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetUserIdByEmail("secretar@secretar.com");

            //Result
            Assert.That(result, Is.EqualTo(3));
        }


        [Test]
        public void HavingUserService_WhenGetUserIdByEmail_ThenReturnRoleList()
        {
            //Arange
            UserService userService = new UserService(mockWrapperRepository.Object);

            //Act
            var result = userService.GetRole();

            //Result
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
