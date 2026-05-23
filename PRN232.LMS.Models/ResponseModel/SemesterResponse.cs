using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Models.ResponseModel
{
    public class SemesterResponse
    {
        public int SemesterId { get; set; }

        public string SemesterName { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<CourseResponse>? Courses { get; set; }
    }
}
