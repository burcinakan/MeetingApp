using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Models;

namespace MeetingApp.Context.Repositories
{
	public class UserRepository : BaseRepository<User>,IUserRepository
	{
		private readonly SQLContext _context;
		public UserRepository(SQLContext context) : base(context)
		{
			_context = context;
		}

		public List<User> GetUserById(Guid userId)
		{
			return _context.Users.Where(u => u.ID == userId).ToList();
		}

		public List<User> GetUsers()
		{
			return _context.Users.ToList();
		}
	}
}
