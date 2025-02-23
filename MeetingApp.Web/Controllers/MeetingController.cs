using MeetingApp.Api.Services;
using MeetingApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

		public MeetingController(IMeetingService meetingService)
		{
			_meetingService = meetingService;
		}
		[HttpGet("GetMeetings")]
		public IActionResult GetMeetings()
		{
			var meetings = _meetingService.GetMeetings();
			return Ok(meetings);
		}

		[HttpGet("GetActiveMeetings")]
		public IActionResult GetActiveMeetings()
		{
			var meetings = _meetingService.GetActiveMeetings();
			return Ok(meetings);
		}

		[HttpGet("GetCanceledMeetings")]
		public  IActionResult GetCanceledMeetings()
		{
			var meetings =  _meetingService.GetCanceledMeetingsAsync();
			return Ok(meetings);
		}
		[HttpPost("CancelMeeting/{meetingId}")]
		public async Task<IActionResult> CancelMeeting(Guid meetingId)
		{
			var meetings = await _meetingService.CancelMeeting(meetingId);
			if (meetings)
			{
				return Ok(new { message = "Toplantı başarıyla iptal edildi." });
			}
			return NotFound(new { message = "Toplantı bulunamadı." });
		}
		[HttpPost("RevertMeeting/{meetingId}")]
		public async Task<IActionResult> RevertMeeting(Guid meetingId)
		{
			var meetings = await _meetingService.RevertMeeting(meetingId);
			if (meetings)
			{
				return Ok(new { message = "Toplantı aktif edildi." });
			}
			return NotFound(new { message = "Toplantı bulunamadı." });
		}

		[HttpPost("AddMeeting")]
		public async Task<IActionResult> AddMeeting([FromBody] MeetingDTO meetingDTO)
		{
			if (meetingDTO == null)
				return BadRequest("Veri hatalı!");

			var result = await _meetingService.Add(meetingDTO);
			if (result)
				return Ok("Başarılı!");

			return StatusCode(500, "Toplantı eklenemedi.");
		}

		[HttpDelete("DeleteMeeting/{meetingId}")]
		public async Task<IActionResult> DeleteMeeting(Guid meetingId)
		{
			var result = await _meetingService.Delete(meetingId);

			if (result)
			{
				return Ok(new { message = "Toplantı başarıyla silindi." });
			}

			return NotFound(new { message = "Toplantı bulunamadı." });
		}

		[HttpPut("UpdateMeeting/{meetingId}")]
		public async Task<IActionResult> UpdateMeeting(Guid meetingId, [FromBody] MeetingDTO meetingDTO)
		{
			var result = await _meetingService.Update(meetingId, meetingDTO);

			if (result)
			{
				return Ok(new { message = "Toplantı başarıyla güncellendi." });
			}

			return NotFound(new { message = "Toplantı bulunamadı." });
		}


	}
}
