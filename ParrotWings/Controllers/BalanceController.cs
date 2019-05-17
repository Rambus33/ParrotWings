using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotWings.Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ParrotWings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        [HttpGet()]
        public async Task<IActionResult> Get([FromServices] IPaymentService paymentService)
        {
            var currentUserId = Convert.ToInt32(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var balance = await paymentService.GetBalanceByUserIdAsync(currentUserId);
            return Ok(Math.Round(balance, 2));
        }
    }
}