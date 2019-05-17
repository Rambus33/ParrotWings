using ParrotWings.Models;
using Microsoft.EntityFrameworkCore;

namespace ParrotWingsTests.Helpers
{
    public static class ContextFactory
    {
        public static RepositoryContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase("ParrotWingsUnitTestsDb")
                .Options;
            var context = new RepositoryContext(options, new DataSeed());
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            return context;
        }
    }    
}