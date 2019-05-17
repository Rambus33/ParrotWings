using Microsoft.EntityFrameworkCore;
using ParrotWings.Models;
using ParrotWings.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParrotWings.Services
{
    public class PaymentService : GenericService<Payment>, IPaymentService
    {
        public PaymentService(IPaymentRepository paymentRepository) : base(paymentRepository)
        { }

        public async Task<double> GetBalanceByUserIdAsync(int userId)
        {
            return await EntityRepository.Query()
                            .Where(x => x.UserId == userId)
                            .SumAsync(y => y.Amount);
        }

        public async Task<IList<Payment>> GetAllByUserIdAsync(int userId)
        {
            var payments = await EntityRepository.Query()
                            .Include(payment => payment.User)
                            .Include(payment => payment.CorrespondentUser)
                            .Where(x => x.UserId == userId)
                            .OrderBy(y => y.Date)
                            .ToListAsync();

            var sumBalance = SumBalance(payments).OrderByDescending(x => x.Date)
                            .ToList();
            return sumBalance;
        }            

        private static IEnumerable<Payment> SumBalance(IList<Payment> payments)
        {
            var result = 0.00;
            foreach (var payment in payments)
            {
                result += payment.Amount;
                payment.Balance = Math.Round(result, 2);
                yield return payment;
            }
        }
    }
}