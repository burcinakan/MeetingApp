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

		public List<User> GetUserById(Guid userId)
		{
			return _userRepository.GetUserById(userId);
		}

		public List<User> GetUsers()
		{
			return _userRepository.GetUsers();
		}
		public bool Delete(Guid ID)
		{
			var meeting = _userRepository.FirstOrDefault(x => x.ID == ID);

			if (meeting == null)
				return false;

			_userRepository.Delete(meeting);

			return true;
		}
	}
}
