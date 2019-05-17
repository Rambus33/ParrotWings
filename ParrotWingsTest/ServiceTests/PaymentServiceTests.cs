using Moq;
using NUnit.Framework;
using ParrotWings.Models;
using ParrotWings.Repository;
using ParrotWings.Services;
using ParrotWingsTests.Helpers;
using System.Threading.Tasks;

namespace ParrotWingsTests.ServiceTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentRepository> paymentRepositoryMock = new Mock<IPaymentRepository>();
        private readonly PaymentService paymentService;

        public PaymentServiceTests()
        {
            paymentService = new PaymentService(paymentRepositoryMock.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            paymentRepositoryMock.Reset();
        }

        [Test]
        public async Task GetBalanceByUserIdAsync_WhenUserIdExist_ReturnBalance()
        {
            // Arrange           
            ArrangePayments();

            // Act
            var result = await paymentService.GetBalanceByUserIdAsync(UserFactory.CurrentUserId);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(550, result);
        }

        [Test]
        public async Task GetAllByUserIdAsync_WhenUserIdExist_ReturnListPaymentWithBalance()
        {
            // Arrange 
            var expected = new[] { 550, 530, 510, 500 };
            ArrangePayments();

            //Act
            var result = await paymentService.GetAllByUserIdAsync(UserFactory.CurrentUserId);

            //Assert
            Assert.NotNull(result);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], result[i].Balance);
            }
        }

        private void ArrangePayments()
        {
            var payments = new DataSeed().GetPayments();
            paymentRepositoryMock
                .Setup(mock => mock.Query())
                .Returns(new AsyncQueryResult<Payment>(payments));
        }
    }
}