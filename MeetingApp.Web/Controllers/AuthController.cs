using MeetingApp.Api.Services;
using MeetingApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}



		[HttpPost("Register")]
		public IActionResult Register([FromBody] RegisterDto userDto)
		{
			try
			{
				var result =  _authService.Register(userDto);
				return Ok(new { message = result });
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}

		[HttpPost("Login")]
		public IActionResult Login([FromBody] LoginDto loginDto)
		{
			try
			{
				var token = _authService.Login(loginDto);
				return Ok(new { token });
			}
			catch (Exception ex)
			{
				return Unauthorized(new { error = ex.Message });
			}
		}

	}
}
