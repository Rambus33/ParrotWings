using Microsoft.EntityFrameworkCore;

namespace ParrotWings.Models
{
    public class RepositoryContext : DbContext
    {
        private readonly IDataSeed dataSeed;

        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public RepositoryContext(DbContextOptions<RepositoryContext> options, IDataSeed dataSeed) : base(options)
        {
            this.dataSeed = dataSeed;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (dataSeed == null)
            {
                modelBuilder.Entity<Payment>()
                            .Property(p => p.Date)
                            .HasDefaultValueSql("getdate()");

                modelBuilder.Entity<User>()
                            .HasData(new User
                            {
                                UserId = 1,
                                Name = "System"
                            });
            }
            else
            {
                modelBuilder.Entity<Payment>()
                            .HasData(dataSeed.GetPayments());
                modelBuilder.Entity<User>()
                            .HasData(dataSeed.GetUsers());
            }
        }
    }
}