using MeetingApp.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;

namespace MeetingApp.Api.Services.Concrete
{
	public class MailService : IMailService
	{
		private readonly EMailSettings _emailSettings;

		public MailService(EMailSettings emailSettings)
		{
			_emailSettings = emailSettings;
		}

		public void SendMeetingInfo(Guid userId, string userEmail, string meetingTitle, DateTime startDate, DateTime endDate, string description, string documentUrl)
		{
			var subject = $"Toplantı Bilgilendirmesi: {meetingTitle}";
			var body = $@"
                Merhaba,

                Toplantı Bilgileri:
                
                Başlık: {meetingTitle}
                Tarih: {startDate.ToString("dd MMM yyyy HH:mm")} - {endDate.ToString("dd MMM yyyy HH:mm")}

                Açıklama: {description}

                Daha fazla bilgi için toplantı dokümanına göz atabilirsiniz: {documentUrl}

            ";
			SendEmail(userEmail, subject, body);
		}

		public void SendWelcomeEmail(string toEmail, string name)
		{
			var subject = "Hoşgeldiniz!";
			var body = $@"
                Merhaba {name},

                Sisteme başarılı bir şekilde kaydoldunuz.

            ";
			SendEmail(toEmail, subject, body);
		}

		private void SendEmail(string toEmail, string subject, string body)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("MeetingApp", _emailSettings.Username));
			message.To.Add(new MailboxAddress("", toEmail));  
			message.Subject = subject;

			var bodyBuilder = new BodyBuilder
			{
				TextBody = body
			};

			message.Body = bodyBuilder.ToMessageBody();

			using (var smtpClient = new SmtpClient())
			{

				smtpClient.Connect(_emailSettings.Host, _emailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
				smtpClient.Authenticate(_emailSettings.Username, _emailSettings.Password);
				smtpClient.Send(message);
				smtpClient.Disconnect(true);
			}
		}

	}
}
