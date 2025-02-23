using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Model.Models
{
    public class Log : BaseEntity
    {
		public string TableName { get; set; }
		public string Action { get; set; } 
		public Guid UserId { get; set; }
		public virtual User User { get; set; }
	}
}
