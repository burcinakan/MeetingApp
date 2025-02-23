using MeetingApp.Model.Models;
using MeetingApp.Models.DTO;

namespace MeetingApp.Api.Services
{
	public interface IAuthService
	{
		string Register(RegisterDto registerDto);
		string Login(LoginDto loginDto);

	}
}
