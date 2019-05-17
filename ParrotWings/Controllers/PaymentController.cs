using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotWings.Dtos;
using ParrotWings.Models;
using ParrotWings.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParrotWings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost()]
        public async Task<IActionResult> Post([FromBody] PaymentDto payment, [FromServices] IPaymentService paymentService, [FromServices] IUserService userService)
        {
            var currentUserId = Convert.ToInt32(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var userBalance = await paymentService.GetBalanceByUserIdAsync(currentUserId);
            if (userBalance < payment.Amount)
            {
                throw new NotEnoughMoneyException("There is not enough money in the account");
            }
            var correspondentUser = await userService.GetByIdAsync(payment.CorrespondentUserId);
            if (correspondentUser == null)
            {
                throw new UserNotFoundException("Recipient user not found");
            }
            var userPayment = new Payment()
            {
                Amount = -payment.Amount,
                UserId = currentUserId,
                CorrespondentUserId = correspondentUser.UserId,
            };
            var correspondentPayment = new Payment()
            {
                Amount = payment.Amount,
                UserId = correspondentUser.UserId,
                CorrespondentUserId = currentUserId,
            };
            paymentService.InsertWithoutSaveChanges(userPayment);
            paymentService.InsertWithoutSaveChanges(correspondentPayment);
            await paymentService.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromServices] IPaymentService paymentService)
        {
            var currentUserId = User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var payments = await paymentService.GetAllByUserIdAsync(Convert.ToInt32(currentUserId));
            return Ok(payments);
        }
    }
}