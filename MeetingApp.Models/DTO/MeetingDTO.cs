﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Models.DTO
{
    public class MeetingDTO
    {
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Description { get; set; }
		public string DocumentUrl { get; set; }

		public Guid UserId { get; set; }
	

	}
}
