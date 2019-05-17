using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParrotWings.Dtos;
using ParrotWings.Models;
using ParrotWings.Services;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;

namespace ParrotWings.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private const double defaultBalance = 500;

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login, [FromServices] IUserService userService)
        {
            var loginUser = await userService.AuthenticateAsync(login.Email, login.Password);
            return Ok(loginUser);
        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> Registration([FromBody] UserDto userParam, [FromServices] IUserService userService, [FromServices] IPaymentService paymentService)
        {
            var user = await userService.RegistrationAsync(userParam.Name, userParam.Email, userParam.Password);
            var initPayment = new Payment
            {
                Amount = defaultBalance,
                UserId = user.UserId,
                CorrespondentUserId = await userService.GetSystemUserIdAsync()
            };
            paymentService.InsertWithoutSaveChanges(initPayment);
            await paymentService.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromServices] IUserService userService)
        {
            var currentUserEmail = User.FindFirst(x => x.Type == ClaimTypes.Email).Value;
            var users = await userService.GetAllAsync();
            var usersWithoutCurrent = users.Where(x => x.Email != currentUserEmail).ToList();
            return Ok(usersWithoutCurrent);
        }
    }
}