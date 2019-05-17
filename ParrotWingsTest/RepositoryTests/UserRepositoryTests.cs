using NUnit.Framework;
using ParrotWings.Models;
using ParrotWings.Repository;
using ParrotWingsTests.Helpers;
using System.Threading.Tasks;

namespace ParrotWingsTests.RepositoryTests
{
    public class UserRepositoryTests
    {
        private UserRepository userRepository;
        private RepositoryContext context;

        [SetUp]
        public void Initialize()
        {
            context = ContextFactory.CreateContext();
            userRepository = new UserRepository(context);
        }

        [TearDown]
        public void Cleanup()
        {
            context.Dispose();
            context = null;
            userRepository = null;
        }

        [Test]
        public async Task GetAsync_WhenExists_ReturnUser()
        {
            // Arrange
            var expected = new User
            {
                Email = "ralph.morrow@mail.com",
                Name = "Ralph Morrow",
                UserId = 3,
                Password = "asdfg"
            };

            // Act
            var result = await userRepository.GetAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(expected.Email, result.Email);
            Assert.AreEqual(expected.UserId, result.UserId);
            Assert.AreEqual(expected.Password, result.Password);
            Assert.AreEqual(expected.Name, result.Name);
        }
    }
}