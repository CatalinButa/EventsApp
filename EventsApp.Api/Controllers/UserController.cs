using EventsApp.Core.Services;
using EventsApp.Database.Dtos.Common;
using EventsApp.Database.Dtos.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Api.Controllers;

namespace EventsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseController
    {
        public UserService userService { get; set; }

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("get-users")]
        public IActionResult GetUsers()
        {
            List<UserDto> userDtos = userService.GetUsers();
            return Ok(userDtos);
        }

        [HttpGet]
        [Route("get-user/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            UserDto userDto = userService.GetUserById(userId);
            return Ok(userDto);
        }

        [HttpPost]
        [Route("register-user")]
        [AllowAnonymous]
        public IActionResult RegisterUser(RegisterRequest registerRequest)
        {
            UserDto registeredUserDto = userService.RegisterUser(registerRequest);
            return Ok(registeredUserDto);
        }

        [HttpPost]
        [Route("login-user")]
        [AllowAnonymous]
        public IActionResult LoginUser(LoginRequest loginRequest)
        {
            string token = userService.LoginUser(loginRequest);
            return Ok(new { token });
        }

        [HttpPut]
        [Route("update-user/{userId}")]
        public IActionResult UpdateUserById(UserDto finalUserDto, int userId)
        {
            int loggedInUserId = GetLoggedInUserId();
            UserDto updatedUserDto = userService.UpdateUserById(finalUserDto, userId, loggedInUserId);
            return Ok(updatedUserDto);
        }

        [HttpDelete]
        [Route("delete-user/{userId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUserById(int userId)
        {
            UserDto deletedUserDto = userService.DeleteUserById(userId);
            return Ok(deletedUserDto);
        }
    }
}
