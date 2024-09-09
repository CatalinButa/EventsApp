using EventsApp.Core.Services;
using EventsApp.Database.Entities;
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
            List<User> users = userService.GetUsers();
            return Ok(users);
        }

        [HttpGet]
        [Route("get-user/{userId}")]
        public IActionResult GetUserById(int userId)
        {
            User user = userService.GetUserById(userId);
            return Ok(user);
        }

        [HttpPost]
        [Route("save-user")]
        public IActionResult SaveUser(User newUser)
        {
            User savedUser = userService.SaveUser(newUser);
            return Ok(savedUser);
        }

        [HttpPut]
        [Route("update-user/{userId}")]
        public IActionResult UpdateUserById(User finalUser, int userId)
        {
            User updatedUser = userService.UpdateUserById(finalUser, userId);
            return Ok(updatedUser);
        }

        [HttpDelete]
        [Route("delete-user/{userId}")]
        public IActionResult DeleteUserById(int userId)
        {
            User deletedUser = userService.DeleteUserById(userId);
            return Ok(deletedUser);
        }
    }
}
