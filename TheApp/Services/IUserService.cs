using MongoDB.Bson;
using TheApp.Model;

namespace TheApp.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(Guid id);

        Task<User> AddUserAsync(User newUser);

        void EditUserAsync(UserDto updatedUser);

        void DeleteUserAsync(UserDto UserToDelete);
    }
}
