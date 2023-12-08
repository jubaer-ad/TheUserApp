using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheApp.Model;

namespace TheApp.Helper
{
    public class JWTHelper
    {
        public static string GenerateJWT(UserDto userDto, string symmetricSecurityKey)
        {
			try
			{
                List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userDto.Email),
                new Claim(ClaimTypes.Role, userDto.Role.ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

                var token = new JwtSecurityToken
                    (
                    claims: claims,
                    signingCredentials: cred,
                    expires: DateTime.Now.AddHours(1)
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
			catch (Exception)
			{

				throw;
			}

        }
    }
}
