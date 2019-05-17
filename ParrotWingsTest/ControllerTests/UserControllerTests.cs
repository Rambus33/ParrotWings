using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ParrotWings;
using ParrotWings.Controllers;
using ParrotWings.Dtos;
using ParrotWings.Models;
using ParrotWings.Services;
using ParrotWingsTests.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParrotWingsTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IPaymentService> paymentServiceMock = new Mock<IPaymentService>();
        private readonly Mock<IUserService> userServiceMock = new Mock<IUserService>();

        private UserController userController;

        [SetUp]
        public void Initialize()
        {
            userController = new UserController
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext() { User = UserFactory.CreateCurrentUser() }
                }
            };
        }

        [TearDown]
        public void Cleanup()
        {
            paymentServiceMock.Reset();
            userServiceMock.Reset();
            userController = null;
        }

        [Test]
        public async Task Login_WhenEmailAndPasswordExist_ReturnOk()
        {
            // Arrange           
            var key = "qwerty123";
            userServiceMock
                .Setup(mock => mock.AuthenticateAsync(It.Is<string>(v => v.Equals(UserFactory.CurrentUserEmail)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserPassword))))
                .ReturnsAsync(new LoginUserDto
                {
                    Email = UserFactory.CurrentUserEmail,
                    Name = UserFactory.CurrentUserName,
                    UserId = UserFactory.CurrentUserId,
                    Token = key
                });
            var login = new LoginDto
            {
                Email = UserFactory.CurrentUserEmail,
                Password = UserFactory.CurrentUserPassword
            };

            // Act
            var result = await userController.Login(login, userServiceMock.Object);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as OkObjectResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [Test]
        public void Login_WhenEmailAndPasswordNotExist_ReturnException()
        {
            // Arrange           
            userServiceMock
                .Setup(mock => mock.AuthenticateAsync(It.Is<string>(v => v.Equals(UserFactory.CurrentUserEmail)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserPassword))))
                .ThrowsAsync(new AuthenticateException("Email or password is incorrect"));
            var login = new LoginDto
            {
                Email = UserFactory.CurrentUserEmail,
                Password = UserFactory.CurrentUserPassword
            };

            // Assert
            Assert.ThrowsAsync<AuthenticateException>(async () => await userController.Login(login, userServiceMock.Object));
        }

        [Test]
        public async Task Registration_WhenUserNotExist_ReturnOk()
        {
            // Arrange
            userServiceMock
                .Setup(mock => mock.RegistrationAsync(It.Is<string>(v => v.Equals(UserFactory.CurrentUserName)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserEmail)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserPassword))))
                .ReturnsAsync(new User
                {
                    Email = UserFactory.CurrentUserEmail,
                    Name = UserFactory.CurrentUserName,
                    UserId = UserFactory.CurrentUserId
                });
            userServiceMock
                .Setup(mock => mock.GetSystemUserIdAsync())
                .ReturnsAsync(UserFactory.SystemUserId);

            // Act
            var result = await userController.Registration(UserDto, userServiceMock.Object, paymentServiceMock.Object);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as OkResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [Test]
        public void Registration_WhenUserExist_ReturnException()
        {
            // Arrange
            userServiceMock
                .Setup(mock => mock.RegistrationAsync(It.Is<string>(v => v.Equals(UserFactory.CurrentUserName)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserEmail)), It.Is<string>(v => v.Equals(UserFactory.CurrentUserPassword))))
                .ThrowsAsync(new RegistrationException("User with the same email already exists"));

            // Assert
            Assert.ThrowsAsync<RegistrationException>(async () => await userController.Registration(UserDto, userServiceMock.Object, paymentServiceMock.Object));
        }

        [Test]
        public async Task GetAll_ReturnUserListWithoutCurrent()
        {
            // Arrange
            var users = new[]
            {
                new User
                {
                    Email = UserFactory.CurrentUserEmail,
                    Name = UserFactory.CurrentUserName,
                    UserId = UserFactory.CurrentUserId
                },
                new User
                {
                    Email = "ralph.morrow@mail.com",
                    Name = "Ralph Morrow",
                    UserId = 3
                },
                new User
                {
                    Email = "jeremy.hawkins@mail.com",
                    Name = "Jeremy Hawkins",
                    UserId = 4
                }
            };
            userServiceMock
                .Setup(mock => mock.GetAllAsync())
                .ReturnsAsync(users);

            // Act
            var result = await userController.GetAll(userServiceMock.Object);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as OkObjectResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
            Assert.AreEqual(users.Where(x => x.UserId != UserFactory.CurrentUserId).ToArray(), createdResult.Value);
        }

        private UserDto UserDto => new UserDto
        {
            Email = UserFactory.CurrentUserEmail,
            Name = UserFactory.CurrentUserName,
            Password = UserFactory.CurrentUserPassword
        };
    }
}