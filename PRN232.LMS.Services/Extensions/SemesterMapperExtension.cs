using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class SemesterMapperExtension
    {
        public static SemesterResponse ToSemesterReponse(this Semester semesters)
        {
            return new SemesterResponse
            {
                SemesterId = semesters.SemesterId,
                SemesterName = semesters.SemesterName,
                StartDate = semesters.StartDate,
                EndDate = semesters.EndDate,
                Courses = semesters.Courses.Select(x => new CourseResponse
                {
                    CourseId = x.CourseId,
                    CourseName = x.CourseName,
                    SemesterId = semesters.SemesterId,
                    SemesterName = semesters.SemesterName
                }).ToList()
            };
        }

        public static List<SemesterResponse> ToSemesterReponseList(this List<Semester> semesters)
        {
            return semesters.Select(x => x.ToSemesterReponse()).ToList();
        }

    }
}
