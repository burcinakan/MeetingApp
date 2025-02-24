using MeetingApp.Model.Models;
using MeetingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Services
{
    public interface IMeetingService
    {
        List<Meeting> GetMeetings();
		List<Meeting> GetMeetingsByUserId(Guid userId);
		List<Meeting> GetCancelMeetingsByUserId(Guid userId);
		List<Meeting> GetCanceledMeetingsAsync();
		List<Meeting> GetActiveMeetings();
		Task<bool> CancelMeeting(Guid meetingId);
		Task<bool> RevertMeeting(Guid meetingId);
		Task<bool> Add(MeetingDTO meetingDTO);
		Task<bool> Update(Guid ID, MeetingDTO meetingDTO);
		Task<bool> Delete(Guid ID);

		Task DeleteCanceledMeetingsAsync(List<Meeting> meetings);


	}
}
