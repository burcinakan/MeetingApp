using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Models;

namespace MeetingApp.Context.Repositories
{
	public class LogRepository : BaseRepository<Log>,ILogRepository
	{
		public LogRepository(SQLContext context) : base(context)
		{
		}
	}
}
