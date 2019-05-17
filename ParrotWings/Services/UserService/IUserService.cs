using ParrotWings.Dtos;
using ParrotWings.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParrotWings.Services
{
    public interface IUserService : IGenericService<User>
    {
        Task<LoginUserDto> AuthenticateAsync(string email, string password);

        Task<User> RegistrationAsync(string name, string email, string password);

        Task<int> GetSystemUserIdAsync();
    }
}