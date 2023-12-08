using MongoDB.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheApp.Model
{
    public class User : Base
    {
        public required string Password { get; set; }


        public static implicit operator UserDto?(User user)
        {
            if (user == null)
            {
                return null;
            }
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsRemoved = user.IsRemoved,
                Phone = user.Phone,
                Role = user.Role
            };
        }
    }
}
