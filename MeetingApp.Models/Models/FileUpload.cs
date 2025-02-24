using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingApp.Models.Models
{
    public class FileUpload
    {
        public IFormFile File { get; set; }

		public string Name { get; set; }

	}
}
