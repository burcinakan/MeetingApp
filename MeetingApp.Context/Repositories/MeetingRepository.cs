using MeetingApp.Api.Repositories;
using MeetingApp.DAL.Contexts;
using MeetingApp.Model.Enums;
using MeetingApp.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingApp.Context.Repositories
{
	public class MeetingRepository : BaseRepository<Meeting>,IMeetingRepository
	{
		private readonly SQLContext _context;
		public MeetingRepository(SQLContext context) : base(context)
		{
			_context = context;
		}

		public async Task<bool> CancelMeeting(Guid meetingId)
		{
			var meeting = await _context.Meetings.FirstOrDefaultAsync(x => x.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}

			meeting.Status = DataStatus.Canceled;
			meeting.ModifiedDate = DateTime.Now;

			_context.Meetings.Update(meeting);
			await _context.SaveChangesAsync(); 

			return true;
		}

		public async Task DeleteCanceledMeetingsAsync(List<Meeting> canceledMeetings)
		{

			_context.Meetings.RemoveRange(canceledMeetings);
			await _context.SaveChangesAsync();
		}

		public async Task<bool> RevertMeeting(Guid meetingId)
		{
			var meeting = await _context.Meetings.FirstOrDefaultAsync(m => m.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}
			meeting.Status = DataStatus.Updated;
			meeting.ModifiedDate = DateTime.Now;

			_context.Meetings.Update(meeting);
			await _context.SaveChangesAsync();

			return true;
		}
	}
}
