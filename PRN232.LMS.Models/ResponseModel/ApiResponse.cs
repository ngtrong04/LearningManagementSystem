using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232.LMS.Models.ResponseModel
{
    public class ApiResponse<T>
    {
        public bool success { get; set; }
        public string message { get; set; }

        public T? Data { get; set; }

        public object? Errors { get; set; }

        public PaginationMetadata pagination { get; set; }
    }
}
