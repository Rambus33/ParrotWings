using NUnit.Framework;
using Moq;
using ParrotWings.Services;
using ParrotWings.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ParrotWingsTests.Helpers;
using System.Threading.Tasks;

namespace ParrotWingsTests
{
    public class BalanceControllerTests
    {
        private readonly Mock<IPaymentService> paymentServiceMock = new Mock<IPaymentService>();

        private BalanceController balanceController;

        [SetUp]
        public void Initialize()
        {
            balanceController = new BalanceController
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
            balanceController = null;
        }

        [Test]
        public async Task Get_WhenUserExists_ReturnBalance()
        {
            // Arrange
            paymentServiceMock
                .Setup(mock => mock.GetBalanceByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(500.003);

            // Act
            var result = await balanceController.Get(paymentServiceMock.Object);

            // Assert
            var createdResult = result as OkObjectResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
            Assert.AreEqual(500.00, createdResult.Value);
        }

        [Test]
        public async Task Get_WhenUserNotExists_ReturnZero()
        {
            // Arrange
            paymentServiceMock
                .Setup(mock => mock.GetBalanceByUserIdAsync(It.Is<int>(v => v.Equals(UserFactory.CurrentUserId))))
                .ReturnsAsync(0);

            // Act
            var result = await balanceController.Get(paymentServiceMock.Object);

            // Assert
            var createdResult = result as OkObjectResult;
            Assert.NotNull(createdResult);
            Assert.AreEqual(200, createdResult.StatusCode);
            Assert.AreEqual(0, createdResult.Value);
        }        
    }
}