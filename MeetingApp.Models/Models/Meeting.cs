using MeetingApp.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Model.Models
{
    public class Meeting : BaseEntity
    {
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public string DocumentUrl { get; set; }
		public Guid UserId { get; set; }

		public User User { get; set; }
	}
}
