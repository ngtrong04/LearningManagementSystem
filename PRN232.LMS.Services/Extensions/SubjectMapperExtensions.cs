using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class SubjectMapperExtensions
    {
        public static SubjectResponse ToSubjectResponse(this Subject subjects)
        {
            return new SubjectResponse
            {
                SubjectId = subjects.SubjectId,
                SubjectName = subjects.SubjectName,
                SubjectCode = subjects.SubjectCode,
                Credit = subjects.Credit ?? 0
            };
        }


        public static IEnumerable<SubjectResponse> ToSubjectResponseList(this IEnumerable<Subject> subjects)
        {
            return subjects.Select(x => x.ToSubjectResponse()).ToList();
        }
    }
}
