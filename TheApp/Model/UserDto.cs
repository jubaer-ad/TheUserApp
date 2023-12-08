using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace TheApp.Model
{
    [Collection("Users")]
    public class UserDto : Base
    {
        public required string PasswordHash { get; set; }
        public static implicit operator User?(UserDto userDto)
        {
            if(userDto == null)
            {
                return null;
            }
            return new()
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Password = string.Empty,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                IsRemoved = userDto.IsRemoved,
                Phone = userDto.Phone,
                Role = userDto.Role
            };
        }
    }
}
