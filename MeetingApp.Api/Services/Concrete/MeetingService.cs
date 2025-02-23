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

		public async Task<bool> Add(MeetingDTO meetingDTO)
		{
			var userExists = await _meetingRepository.FirstOrDefaultAsync(u => u.ID == meetingDTO.UserId
			&& u.StartDate == meetingDTO.StartDate
			);
			if (userExists != null)
			{
				throw new Exception("Belirtilen tarihte bu kullanıcının başka toplantısı mevcut!");
			}

			var user = await _userRepository.FirstOrDefaultAsync(u => u.ID == meetingDTO.UserId);

			var meeting = new Meeting
			{
				Title = meetingDTO.Title,
				StartDate = meetingDTO.StartDate,
				EndDate = meetingDTO.EndDate,
				Description = meetingDTO.Description,
				DocumentUrl = meetingDTO.DocumentUrl,
				UserId = meetingDTO.UserId
			};
			await _meetingRepository.AddAsync(meeting);

			if (user != null)
			{
				var dsc = "Toplantınız başarılı bir şekilde oluşturulmuştur!";
				_mailService.SendMeetingInfo(meeting.UserId, user.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);

			}

			return true;
		}

		public async Task<bool> CancelMeeting(Guid meetingId)
		{
			var meeting = await _meetingRepository.FirstOrDefaultAsync(x => x.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}
			meeting.Status = DataStatus.Canceled;
			meeting.ModifiedDate = DateTime.Now;

			var users = await _userRepository.FirstOrDefaultAsync(u => u.ID == meeting.UserId);
			var dsc = "Toplantınız iptal edilmiştir!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);


			await _meetingRepository.UpdateAsync(meeting);

			return true;
		}

		public async Task<bool> Delete(Guid ID)
		{
			var meeting = await  _meetingRepository.FirstOrDefaultAsync(x => x.ID == ID);

			if (meeting == null)
				return false;

			await _meetingRepository.DeleteAsync(meeting);

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

		public async Task<bool> RevertMeeting(Guid meetingId)
		{
			var meeting = await _meetingRepository.FirstOrDefaultAsync(m => m.ID == meetingId);
			if (meeting == null)
			{
				return false;
			}

			meeting.Status = DataStatus.Updated;
			meeting.ModifiedDate = DateTime.Now;

			var users = await _userRepository.FirstOrDefaultAsync(u => u.ID == meeting.UserId);
			var dsc = "Toplantınız başarılı bir şekilde yeniden oluşturulmuştur!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);



			await _meetingRepository.UpdateAsync(meeting);
			return true;
		}

		public async Task<bool> Update(Guid id, MeetingDTO meetingDTO)
		{
			var meeting = await _meetingRepository.FirstOrDefaultAsync(x => x.ID == id);

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

			var users = await _userRepository.FirstOrDefaultAsync(u => u.ID == meeting.UserId);
			var dsc = "Toplantınızda güncellemeler oluşturulmuştur!";
			_mailService.SendMeetingInfo(meeting.UserId, users.Email, meeting.Title, meeting.StartDate, meeting.EndDate, dsc, meeting.DocumentUrl);



			await _meetingRepository.UpdateAsync(meeting);
			return true;

		}
	}
}
