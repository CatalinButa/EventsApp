using EventsApp.Core.Services;
using EventsApp.Database.Dtos.Common;
using Microsoft.AspNetCore.Mvc;

namespace EventsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
        [Route("save-user")]
        public IActionResult SaveUser(UserDto newUserDto)
        {
            UserDto savedUserDto = userService.SaveUser(newUserDto);
            return Ok(savedUserDto);
        }

        [HttpPut]
        [Route("update-user/{userId}")]
        public IActionResult UpdateUserById(UserDto finalUserDto, int userId)
        {
            UserDto updatedUserDto = userService.UpdateUserById(finalUserDto, userId);
            return Ok(updatedUserDto);
        }

        [HttpDelete]
        [Route("delete-user/{userId}")]
        public IActionResult DeleteUserById(int userId)
        {
            UserDto deletedUserDto = userService.DeleteUserById(userId);
            return Ok(deletedUserDto);
        }
    }
}
