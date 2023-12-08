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

        public async Task<User> AddUserAsync(User newUser)
        {
            try
            {
                var res = _dbContext.Users.FirstOrDefault(x => x.Email == newUser.Email && x.IsRemoved == false);
                if (res != null)
                {
                    return null;
                }

                var addRes = await _dbContext.Users.AddAsync(newUser);
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

        public void DeleteUserAsync(UserDto userDelete)
        {
            try
            {
                var userToDelete = _dbContext.Users.FirstOrDefault(u => u.Id == userDelete.Id);

                if (userToDelete != null)
                {
                    userToDelete = userDelete;
                    userToDelete.IsRemoved = true;

                    _dbContext.Users.Update(userToDelete);

                    _dbContext.ChangeTracker.DetectChanges();
                    Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentException("User not found to delete");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void EditUserAsync(UserDto updatedUser)
        {
            try
            {
                var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.Id == updatedUser.Id);

                if (userToUpdate != null)
                {
                    userToUpdate = updatedUser;

                    _dbContext.Users.Update(userToUpdate);

                    _dbContext.ChangeTracker.DetectChanges();
                    Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);

                    _dbContext.SaveChanges();
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

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                List<User> users = new();
                var res = _dbContext.Users.OrderBy(u => u.Id).AsNoTracking().AsEnumerable().ToList();
                foreach(var userDto in res)
                {
                    users.Add(userDto);
                }
                return users;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User?> GetUserByIdAsync(ObjectId id)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
