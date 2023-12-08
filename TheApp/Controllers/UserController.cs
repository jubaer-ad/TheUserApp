using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TheApp.Model;
using TheApp.Services;

namespace TheApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            try
            {
                var res = await _userService.AddUserAsync(user);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var res = await _userService.GetUserByIdAsync(id);
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var res = await _userService.GetAllUsersAsync();
                return Ok(res);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete]
        public Task<IActionResult> DeleteUser(User user)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut]
        public Task<IActionResult> EditUser(User user)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
