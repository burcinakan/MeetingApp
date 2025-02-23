using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Model.Models
{
    public class User : BaseEntity
    {
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public byte[] PasswordSalt { get; set; }
		public byte[] PasswordHash { get; set; }
		public string ProfileImage { get; set; }

		public ICollection<Meeting> Meetings { get; set; } = new List<Meeting>();

	}
}
