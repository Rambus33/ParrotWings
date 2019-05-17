using ParrotWings.Models;

namespace ParrotWings.Repository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(RepositoryContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}