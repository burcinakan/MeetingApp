using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Enums;
using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Context.Repositories
{
	public class MeetingRepository : BaseRepository<Meeting>,IMeetingRepository
	{

		public MeetingRepository(SQLContext context) : base(context)
		{
		
		}

	}
}
