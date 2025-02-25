using MeetingApp.Api.Repositories;
using MeetingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Services.Concrete
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User> GetUserById(Guid userId)
		{
			var user = await _userRepository.FirstOrDefaultAsync(x => x.ID == userId);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			return user;
		}

		public List<User> GetUsers()
		{
			return _userRepository.GetAll();
		}
		public async Task<bool> Delete(Guid ID)
		{
			var meeting = await _userRepository.FirstOrDefaultAsync(x => x.ID == ID);

			if (meeting == null)
				return false;

			await _userRepository.DeleteAsync(meeting);

			return true;
		}
	}
}
