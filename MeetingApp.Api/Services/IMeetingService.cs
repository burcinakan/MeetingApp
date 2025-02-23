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
		bool CancelMeeting(Guid meetingId);
		bool RevertMeeting(Guid meetingId);
		List<Meeting> GetCanceledMeetings();
		List<Meeting> GetActiveMeetings();

		bool Add(MeetingDTO meetingDTO);
		bool Update(Guid ID, MeetingDTO meetingDTO);
		bool Delete(Guid ID);


	}
}
