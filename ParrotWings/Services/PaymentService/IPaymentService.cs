using ParrotWings.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParrotWings.Services
{
    public interface IPaymentService : IGenericService<Payment>
    {
        Task<double> GetBalanceByUserIdAsync(int userId);

        Task<IList<Payment>> GetAllByUserIdAsync(int userId);
    }
}