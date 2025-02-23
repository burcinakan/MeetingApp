using MeetingApp.Api.Repositories;
using MeetingApp.Model.Enums;
using MeetingApp.Model.Models;
using MeetingApp.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Api.Services.Concrete
{
	public class MeetingService : IMeetingService
	{
		IMeetingRepository _meetingRepository;
		IUserRepository _userRepository;
		IMailService _mailService;

		public MeetingService(IMeetingRepository meetingRepository, IMailService mailService, IUserRepository userRepository)
		{
			_meetingRepository = meetingRepository;
			_mailService = mailService;
			_userRepository = userRepository;
		}

		public bool Add(MeetingDTO meetingDTO)
		{
			var userExists = _meetingRepository.FirstOrDefault(u => u.ID == meetingDTO.UserId
			&& u.StartDate == meetingDTO.StartDate
			);
			if (userExists != null)
			{
				throw new Exception("Belirtilen tarihte bu kullanıcının başka toplantısı mevcut!");
			}
			var users = _userRepository.FirstOrDefault(u => u.ID == meetingDTO.UserId);
			var meeting = new Meeting
			{
				Title = meetingDTO.Title,
				StartDate = meetingDTO.StartDate,
				EndDate = meetingDTO.EndDate,
				Description = meetingDTO.Description,
				DocumentUrl = meetingDTO.DocumentUrl,
				UserId = meetingDTO.UserId
			};
			_meetingRepository.Add(meeting);

			var dsc = "Toplantınız başarılı bir şekilde oluşturulmuştur!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);

			return true;
		}

		public bool CancelMeeting(Guid meetingId)
		{
			var meeting = _meetingRepository.FirstOrDefault(x => x.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}
			meeting.Status = DataStatus.Canceled;
			meeting.ModifiedDate = DateTime.Now;

			var users = _userRepository.FirstOrDefault(u => u.ID == meeting.UserId);
			var dsc = "Toplantınız iptal edilmiştir!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);


			_meetingRepository.Update(meeting);

			return true;
		}

		public bool Delete(Guid ID)
		{
			var meeting = _meetingRepository.FirstOrDefault(x => x.ID == ID);

			if (meeting == null)
				return false;

			_meetingRepository.Delete(meeting);

			return true;
		}

		public List<Meeting> GetActiveMeetings()
		{
			return _meetingRepository.Where(m => m.Status != DataStatus.Canceled).ToList();
		}

		public List<Meeting> GetCanceledMeetings()
		{
			return _meetingRepository.Where(m => m.Status != DataStatus.Canceled).ToList();
		}

		public List<Meeting> GetMeetings()
		{
			return _meetingRepository.GetAll().ToList();
		}

		public bool RevertMeeting(Guid meetingId)
		{
			var meeting = _meetingRepository.FirstOrDefault(m => m.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}

			meeting.Status = DataStatus.Updated;
			meeting.ModifiedDate = DateTime.Now;

			var users = _userRepository.FirstOrDefault(u => u.ID == meeting.UserId);
			var dsc = "Toplantınız başarılı bir şekilde yeniden oluşturulmuştur!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);



			_meetingRepository.Update(meeting);
			return true;
		}

		public bool Update(Guid id, MeetingDTO meetingDTO)
		{
			var meeting = _meetingRepository.FirstOrDefault(x => x.ID == id);

			if (meeting == null)
			{
				return false;
			}

			meeting.Title = meetingDTO.Title;
			meeting.StartDate = meetingDTO.StartDate;
			meeting.EndDate = meetingDTO.EndDate;
			meeting.Description = meetingDTO.Description;
			meeting.DocumentUrl = meetingDTO.DocumentUrl;
			meeting.UserId = meetingDTO.UserId;

			var users = _userRepository.FirstOrDefault(u => u.ID == meeting.UserId);
			var dsc = "Toplantınızda güncellemeler oluşturulmuştur!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);



			_meetingRepository.Update(meeting);
			return true;

		}
	}
}
