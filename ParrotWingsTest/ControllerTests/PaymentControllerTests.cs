using NUnit.Framework;
using Moq;
using ParrotWings.Controllers;
using ParrotWings.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ParrotWingsTests.Helpers;
using System.Threading.Tasks;
using ParrotWings.Dtos;
using ParrotWings.Models;
using ParrotWings;
using System;

namespace ParrotWingsTests.ControllerTests
{
    public class PaymentControllerTests
    {
        private readonly Mock<IPaymentService> paymentServiceMock = new Mock<IPaymentService>();
        private readonly Mock<IUserService> userServiceMock = new Mock<IUserService>();

        private PaymentController paymentController;

        [SetUp]
        public void Initialize()
        {
            paymentController = new PaymentController
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
            paymentController = null;
        }

        [Test]
        public async Task Post_WhenUserBalanceMoreThanAmountAndCorrespondentUserExists_ReturnOk()
        {
            // Arrange           
            paymentServiceMock
                .Setup(mock => mock.GetBalanceByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(500.003);
            userServiceMock
                .Setup(mock => mock.GetByIdAsync(It.Is<int>(v => v.Equals(UserFactory.CorrespondentUserId))))
                .ReturnsAsync(new User
                {
                    Email = "ralph.morrow@mail.com",
                    Name = "Ralph Morrow",
                    UserId = UserFactory.CorrespondentUserId
                });

            // Act
            var result = await paymentController.Post(PaymentDto, paymentServiceMock.Object, userServiceMock.Object);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as OkResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        [Test]
        public void Post_WhenUserBalanceNoMoreThanAmount_ReturnException()
        {
            // Arrange
            paymentServiceMock
                .Setup(mock => mock.GetBalanceByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(100.003);

            // Assert
            Assert.ThrowsAsync<NotEnoughMoneyException>(async () => await paymentController.Post(PaymentDto, paymentServiceMock.Object, userServiceMock.Object));
        }

        [Test]
        public void Post_WhenCorrespondentUserNotFound_ReturnException()
        {
            // Arrange           
            paymentServiceMock
                .Setup(mock => mock.GetBalanceByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(500.003);

            // Assert
            Assert.ThrowsAsync<UserNotFoundException>(async () => await paymentController.Post(PaymentDto, paymentServiceMock.Object, userServiceMock.Object));
        }

        [Test]
        public async Task GetAll_WhenUserExists_ReturnPayments()
        {
            var payments = new[]
            {
                new Payment
                {
                    Amount = 10,
                    CorrespondentUserId = 4,
                    Date = new DateTime(2019,05,01),
                    PaymentId = 1,
                    UserId = UserFactory.CurrentUserId
                },
                new Payment
                {
                    Amount = 20,
                    CorrespondentUserId = 3,
                    Date = new DateTime(2019,04,01),
                    PaymentId = 2,
                    UserId = UserFactory.CurrentUserId
                }
            };
            paymentServiceMock
                .Setup(mock => mock.GetAllByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(payments);
            var result = await paymentController.GetAll(paymentServiceMock.Object);

            var createdResult = result as OkObjectResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
        }

        private PaymentDto PaymentDto => new PaymentDto
        {
            Amount = 200,
            CorrespondentUserId = UserFactory.CorrespondentUserId
        };
    }
}