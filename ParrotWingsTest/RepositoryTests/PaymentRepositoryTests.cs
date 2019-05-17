using NUnit.Framework;
using ParrotWings.Models;
using ParrotWings.Repository;
using ParrotWingsTests.Helpers;
using System;
using System.Threading.Tasks;

namespace ParrotWingsTests.RepositoryTests
{
    public class PaymentRepositoryTests
    {
        private PaymentRepository paymentRepository;
        private RepositoryContext context;

        [SetUp]
        public void Initialize()
        {
            context = ContextFactory.CreateContext();
            paymentRepository = new PaymentRepository(context);
        }

        [TearDown]
        public void Cleanup()
        {
            context.Dispose();
            context = null;
            paymentRepository = null;
        }

        [Test]
        public async Task GetAsync_WhenExists_ReturnPayment()
        {
            // Arrange
            var expected = new Payment
            {
                Amount = 500,
                CorrespondentUserId = 1,
                Date = new DateTime(2019, 4, 19, 12, 19, 1),
                PaymentId = 1,
                UserId = UserFactory.CurrentUserId
            };
            // Act
            var result = await paymentRepository.GetAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expected.PaymentId, result.PaymentId);
            Assert.AreEqual(expected.UserId, result.UserId);
            Assert.AreEqual(expected.CorrespondentUserId, result.CorrespondentUserId);
            Assert.AreEqual(expected.Amount, result.Amount);
            Assert.AreEqual(expected.Date, result.Date);
        }
    }
}