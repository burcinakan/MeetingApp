using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Services
{
    public interface IMailService
    {
        void SendWelcomeEmail(string toEmail, string name);

        void SendMeetingInfo(Guid userId, string userEmail, string meetingTitle, DateTime startDate, DateTime endDate, string description, string documentUrl);

    }
}
