using MeetingApp.Api.Services;
using MeetingApp.Api.Services.Concrete;
using MeetingApp.Models.DTO;
using MeetingApp.Models.Models;
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
		[HttpGet("GetMeetingsByUserId/{userId}")]
		public IActionResult GetMeetingsByUserId(Guid userId)
		{
			var meetings = _meetingService.GetMeetingsByUserId(userId);
			if (meetings.Count > 0)
			{
				return Ok(meetings);
			}
			return NotFound(new { message = "Toplantı bulunamadı." });
		}

		[HttpGet("GetCancelMeetingsByUserId/{userId}")]
		public IActionResult GetCancelMeetingsByUserId(Guid userId)
		{
			var meetings = _meetingService.GetCancelMeetingsByUserId(userId);
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
				return Ok(new { message = "Başarılı!" });

			return StatusCode(500, new { message = "Toplantı eklenemedi." });
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
				return Ok(new { message = "Başarılı!" });
			}

			return NotFound(new { message = "Toplantı bulunamadı." });
		}

		[HttpPost("UploadFile"), DisableRequestSizeLimit]
		public async Task<IActionResult> UploadFile([FromForm] FileUpload file)
		{
			if (file.File == null || file.File.Length == 0)
			{
				return BadRequest("Dosya boş olamaz.");
			}

			var folderName = Path.Combine("Resources", "AllFiles");
			var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

			if (!Directory.Exists(pathToSave))
			{
				Directory.CreateDirectory(pathToSave);
			}

			var fileName = file.File.FileName;
			var fileExtension = Path.GetExtension(fileName);
			var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fileName);
			var fullPath = Path.Combine(pathToSave, fileName);
			var dbPath = Path.Combine(folderName, fileName);

			if (System.IO.File.Exists(fullPath))
			{
				var randomSuffix = Guid.NewGuid().ToString().Substring(0, 8); 
				fileName = $"{fileNameWithoutExt}_{randomSuffix}{fileExtension}";
				fullPath = Path.Combine(pathToSave, fileName);
				dbPath = Path.Combine(folderName, fileName);
			}

			using (var stream = new FileStream(fullPath, FileMode.Create))
			{
				await file.File.CopyToAsync(stream);
			}

			return Ok(new { dbPath });
		}
	}
}
