using System.Collections.Generic;
using System.Security.Claims;

namespace ParrotWingsTests.Helpers
{
    public static class UserFactory
    {
        public const int SystemUserId = 1;

        public const int CorrespondentUserId = 3;

        public const int CurrentUserId = 2;
        public const string CurrentUserName = "Matthew Betz";
        public const string CurrentUserEmail = "matthew.betz@mail.com";
        public const string CurrentUserPassword = "asd345";

        public static ClaimsPrincipal CreateCurrentUser()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, CurrentUserName),
                new Claim(ClaimTypes.NameIdentifier, CurrentUserId.ToString()),
                new Claim(ClaimTypes.Email, CurrentUserEmail)
            };
            var identity = new ClaimsIdentity(claims);
            return new ClaimsPrincipal(identity);
        }
    }
}