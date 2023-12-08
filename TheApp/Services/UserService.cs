using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using System.Linq;
using TheApp.Model;
using TheApp.SystemDbContext;

namespace TheApp.Services
{
    public class UserService(SystemDbContextClass dbContext) : IUserService
    {
        private readonly SystemDbContextClass _dbContext = dbContext;

        public async Task<UserDto> AddUserAsync(User newUser)
        {
            try
            {
                var res = _dbContext.Users.FirstOrDefault(x => x.Email == newUser.Email && x.IsRemoved == false);
                if (res != null)
                {
                    return null;
                }
                UserDto dto = newUser;
                dto.Id = Guid.NewGuid();
                var addRes = await _dbContext.Users.AddAsync(dto);
                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
                await _dbContext.SaveChangesAsync();

                return await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email && x.IsRemoved == false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserDto DeleteUserAsync(Guid id)
        {
            try
            {
                var userToDelete = _dbContext.Users.FirstOrDefault(u => u.Id == id && u.IsRemoved == false);
                if (userToDelete == null)
                {
                    throw new ArgumentException("User not found to delete");
                }

                userToDelete.IsRemoved = true;

                _dbContext.Users.Update(userToDelete);

                _dbContext.ChangeTracker.DetectChanges();
                Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

                _dbContext.SaveChanges();

                return userToDelete;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto> EditUserAsync(UserDto updatedUser)
        {
            try
            {
                var res = _dbContext.Users.FirstOrDefault(x => x.Email == updatedUser.Email && x.IsRemoved == false);
                if (res != null)
                {
                    return null;
                }
                var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.Id == updatedUser.Id && u.IsRemoved == false);

                if (userToUpdate != null)
                {
                    userToUpdate.FirstName = updatedUser.FirstName;
                    userToUpdate.LastName = updatedUser.LastName;
                    userToUpdate.Email = updatedUser.Email;
                    userToUpdate.Phone = updatedUser.Phone;
                    userToUpdate.Role = updatedUser.Role;

                    _dbContext.Users.Update(userToUpdate);

                    _dbContext.ChangeTracker.DetectChanges();
                    Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

                    _dbContext.SaveChanges();

                    return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == updatedUser.Email && u.IsRemoved == false);
                }
                else
                {
                    throw new ArgumentException("User not found to update");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            try
            {
                var res = _dbContext.Users.Where(u => u.IsRemoved == false).OrderBy(u => u.Id).AsNoTracking().AsEnumerable().ToList();
                foreach(var userDto in res)
                {
                    userDto.PasswordHash = string.Empty;
                }
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserDto?> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && u.IsRemoved == false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> Login(LoginReq loginReq)
        {
            try
            {
                var userInDB = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginReq.Email && u.IsRemoved == false);
                if (userInDB == null) return null;
                if (!BCrypt.Net.BCrypt.Verify(loginReq.Password, userInDB.PasswordHash)) return "Email or password is wrong";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
