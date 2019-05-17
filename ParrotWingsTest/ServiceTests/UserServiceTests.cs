using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using ParrotWings;
using ParrotWings.Classes;
using ParrotWings.Models;
using ParrotWings.Repository;
using ParrotWings.Services;
using ParrotWingsTests.Helpers;
using System.Threading.Tasks;

namespace ParrotWingsTests.ServiceTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        private readonly Mock<IOptions<AuthSettings>> authSettingsMock = new Mock<IOptions<AuthSettings>>();
        private readonly UserService userService;

        public UserServiceTests()
        {
            authSettingsMock.Setup(mock => mock.Value)
                .Returns(new AuthSettings() { Key = "5d5b3235a8b413c3cab5c3f4f65c07fcalskd234n1k507ae" });
            userService = new UserService(authSettingsMock.Object, userRepositoryMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            userRepositoryMock.Reset();
        }

        [Test]
        public async Task AuthenticateAsync_WhenEmailAndPasswordExist_ReturnLoginUserDto()
        {
            // Arrange 
            ArrangeUsers();

            // Act
            var result = await userService.AuthenticateAsync(UserFactory.CurrentUserEmail, UserFactory.CurrentUserPassword);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(UserFactory.CurrentUserEmail, result.Email);
            Assert.AreEqual(UserFactory.CurrentUserName, result.Name);
            Assert.AreEqual(UserFactory.CurrentUserId, result.UserId);
            Assert.NotNull(result.Token);
        }

        [Test]
        public void AuthenticateAsync_WhenEmailAndPasswordNotExist_ReturnException()
        {
            // Arrange 
            ArrangeUsers();

            //Assert
            Assert.ThrowsAsync<AuthenticateException>(async () =>await userService.AuthenticateAsync("test@test.com", UserFactory.CurrentUserPassword));
        }

        [Test]
        public async Task RegistrationAsync_WhenEmailNotExist_ReturnUser()
        {
            // Arrange 
            ArrangeUsers();
            var newUserName = "test";
            var newUserEmail = "test@test.com";
            var NewUserPassword = "test123";

            // Act
            var result = await userService.RegistrationAsync(newUserName, newUserEmail, NewUserPassword);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(newUserName, result.Name);
            Assert.AreEqual(newUserEmail, result.Email);
            Assert.AreEqual(NewUserPassword, result.Password);
        }

        [Test]
        public void RegistrationAsync_WhenEmailExist_ReturnException()
        {
            // Arrange 
            ArrangeUsers();

            //Assert
            Assert.ThrowsAsync<RegistrationException>(async () => await userService.RegistrationAsync(UserFactory.CurrentUserName, UserFactory.CurrentUserEmail, UserFactory.CurrentUserPassword));
        }

        private void ArrangeUsers()
        {
            var users = new DataSeed().GetUsers();
            userRepositoryMock
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<User>(users));
        }
    }
}