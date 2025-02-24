using MeetingApp.Api.Services;
using MeetingApp.Models.DTO;
using MeetingApp.Models.Models;
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


		[HttpPost("UploadImage"), DisableRequestSizeLimit]
		public async Task<IActionResult> UploadImage([FromForm] FileUpload file)
		{
			if (file.File == null || file.File.Length == 0)
			{
				return BadRequest("Dosya boş olamaz.");
			}

			var folderName = Path.Combine("Resources", "AllFiles");
			var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

			if (!Directory.Exists(pathToSave))
			{
				Directory.CreateDirectory(pathToSave);
			}

			var fileName = file.File.FileName;
			var fileExtension = Path.GetExtension(fileName);
			var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
			var fullPath = Path.Combine(pathToSave, fileName);
			var dbPath = Path.Combine(folderName, fileName);

			if (System.IO.File.Exists(fullPath))
			{
				var randomSuffix = Guid.NewGuid().ToString().Substring(0, 8);
				fileName = $"{fileNameWithoutExt}_{randomSuffix}{fileExtension}";
				fullPath = Path.Combine(pathToSave, fileName);
				dbPath = Path.Combine(folderName, fileName);
			}

			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await file.File.CopyToAsync(stream);
			}

			return Ok(new { dbPath });
		}

	}
}
