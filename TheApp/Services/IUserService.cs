using MongoDB.Bson;
using TheApp.Model;

namespace TheApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);

        Task<UserDto> AddUserAsync(User newUser);

        Task<UserDto> EditUserAsync(UserDto updatedUser);

        UserDto DeleteUserAsync(Guid id);
        Task<dynamic> LoginAsync(LoginReq loginReq);

    }
}
