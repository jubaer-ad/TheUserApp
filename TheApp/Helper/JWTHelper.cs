using System.Security.Claims;
using TheApp.Model;

namespace TheApp.Helper
{
    public class JWTHelper
    {
        public static GenerateJWT(UserDto userDto, string symmetricSecurityKey)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, userDto.Role.ToString())
            };
        }
    }
}
