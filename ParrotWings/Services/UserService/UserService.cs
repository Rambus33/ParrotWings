using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParrotWings.Classes;
using ParrotWings.Dtos;
using ParrotWings.Models;
using ParrotWings.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParrotWings.Services
{    
    public class UserService : GenericService<User>, IUserService
    {
        private readonly AuthSettings authSettings;
        private const double DefaultBalance = 500.00;

        public UserService(IOptions<AuthSettings> authSettings, IUserRepository userRepository) : base(userRepository)
        {
            this.authSettings = authSettings.Value;
        }

        public async Task<LoginUserDto> AuthenticateAsync(string email, string password)
        {
            var user = await EntityRepository.Query()
                            .SingleOrDefaultAsync(x => x.Email == email 
                                        && x.Password == password);

            if (user == null)
            {
                throw new AuthenticateException("Email or password is incorrect");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),                   
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginUserDto { Email = user.Email, Name = user.Name, UserId = user.UserId, Token = tokenHandler.WriteToken(token) };
        }

        public async Task<User> RegistrationAsync(string name, string email, string password)
        {
            var user = await EntityRepository.Query()
                            .SingleOrDefaultAsync(x => x.Email == email);
            if (user != null)
            {
                throw new RegistrationException("User with the same email already exists");
            }

            var newUser = new User
            {
                Email = email,
                Name = name,
                Password = password
            };
            EntityRepository.InsertWithoutSaveChanges(newUser);
            await EntityRepository.SaveChangesAsync();
            return newUser;
        }

        public new async Task<IList<User>> GetAllAsync()
        {
            var users = await base.GetAllAsync();
            return users.Where(a => !string.IsNullOrWhiteSpace(a.Email))
                            .ToList();
        }

        public async Task<int> GetSystemUserIdAsync()
        {
            var user = await EntityRepository.Query()
                             .SingleOrDefaultAsync(x => x.Name == "System" 
                                        && string.IsNullOrEmpty(x.Password)
                                        && string.IsNullOrEmpty(x.Email));
            if (user == null)
            {
                throw new UserNotFoundException("System user not found");
            }
            return user.UserId;
        }
    }
}