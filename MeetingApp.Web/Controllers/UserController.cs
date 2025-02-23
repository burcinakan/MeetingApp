using MeetingApp.Api.Services;
using MeetingApp.Api.Services.Concrete;
using MeetingApp.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpGet("GetUsers")]
		public IActionResult GetUsers()
		{
			var users = _userService.GetUsers();
		    return Ok(users);
		}
		[HttpGet("GetUserById")]
		public IActionResult GetUserById(Guid userId)
		{
			var users = _userService.GetUserById(userId);
			return Ok(users);
		}

		[HttpDelete("DeleteUser/{userId}")]
		public IActionResult DeleteUser(Guid userId)
		{
			var result = _userService.Delete(userId);

			if (result)
			{
				return Ok(new { message = "Kullanıcı başarıyla silindi." });
			}

			return NotFound(new { message = "Kullanıcı bulunamadı." });
		}

	}
}
