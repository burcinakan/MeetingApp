using MeetingApp.Model.Models;
using MeetingApp.Models.DTO;

namespace MeetingApp.Api.Services
{
	public interface IAuthService
	{
		Task<string> Register(RegisterDto registerDto);
		Task<string> Login(LoginDto loginDto);

	}
}
