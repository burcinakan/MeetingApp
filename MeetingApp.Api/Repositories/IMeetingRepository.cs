using MeetingApp.Model.Models;

namespace MeetingApp.Api.Repositories
{
	public interface IMeetingRepository : IBaseRepository<Meeting> 
	{
		Task DeleteCanceledMeetingsAsync(List<Meeting> canceledMeetings);

		Task<bool> CancelMeeting(Guid meetingId);
		Task<bool> RevertMeeting(Guid meetingId);

	}
}
