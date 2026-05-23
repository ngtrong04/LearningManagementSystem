using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Models.RequestModel
{
    public class QueryParameters
    {
        public string? Search { get; set; }

        public string? Sort { get; set; }

        public int Page { get; set; } = 1;

        public int Size { get; set; } = 10;

        public string? Fields { get; set; }

        public string? Expand { get; set; }
    }
}
