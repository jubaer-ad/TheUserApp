using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using System.Net;
using TheApp.Model;
using TheApp.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
                if (res != null)
                {
                    return Ok(new StandardApiResponse()
                    {
                        Data = res,
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK
                    });
                }
                return Ok(new StandardApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User already is the DB"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(Guid id)
        {
            try
            {
                var res = await _userService.GetUserByIdAsync(id);
                if (res != null)
                {
                    return Ok(new StandardApiResponse()
                    {
                        Data = res,
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK
                    });
                }
                return Ok(new StandardApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest,
                    Message = "User not found"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var res = await _userService.GetAllUsersAsync();
                return Ok(new StandardApiResponse()
                {
                    Data = res,
                    IsSuccess = true,
                    StatusCode = HttpStatusCode.OK
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var res = _userService.DeleteUserAsync(id);
                if (res != null)
                {
                    return Ok(new StandardApiResponse()
                    {
                        Data = res,
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK
                    });
                }
                return Ok(new StandardApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> EditUser(UserDto user)
        {
            try
            {
                var res = await _userService.EditUserAsync(user);
                if (res != null)
                {
                    return Ok(new StandardApiResponse()
                    {
                        Data = res,
                        IsSuccess = true,
                        StatusCode = HttpStatusCode.OK
                    });
                }
                return Ok(new StandardApiResponse()
                {
                    IsSuccess = false,
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginReq loginReq)
        {
            try
            {
                var res = await _userService.LoginAsync(loginReq);
                return StatusCode((int)res.StatusCode, Newtonsoft.Json.JsonConvert.SerializeObject(res));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
