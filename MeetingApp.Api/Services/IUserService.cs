using MeetingApp.Api.Repositories;
using MeetingApp.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Services
{
    public interface IUserService 
    {
		List<User> GetUsers();

		List<User> GetUserById(Guid userId);

		bool Delete(Guid ID);
	}
}
