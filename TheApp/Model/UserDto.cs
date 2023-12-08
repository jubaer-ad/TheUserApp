using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TheApp.Model
{
    [Collection("Users")]
    public class UserDto : Base
    {
        public Guid Id { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsRemoved { get; set; } = false;
        public static implicit operator User?(UserDto userDto)
        {
            if(userDto == null)
            {
                return null;
            }
            return new()
            {
                Email = userDto.Email,
                Password = string.Empty,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Phone = userDto.Phone,
                Role = userDto.Role
            };
        }
    }
}
