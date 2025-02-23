﻿using MeetingApp.Model.Models;

namespace MeetingApp.Api.Repositories
{
	public interface IUserRepository : IBaseRepository<User> 
	{
		List<User> GetUsers();

		List<User> GetUserById(Guid userId);
	}
}
