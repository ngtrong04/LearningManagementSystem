using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Models.RequestModel
{
    public class UpdateCourseRequest
    {
        public string CourseName { get; set; } = string.Empty;

        public int SemesterId { get; set; }
    }
}
