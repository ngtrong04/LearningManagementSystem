using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Models.ResponseModel
{
    public class SubjectResponse
    {
        public int SubjectId { get; set; }

        public string SubjectCode { get; set; }

        public string SubjectName { get; set; }

        public int Credit { get; set; }
    }
}
