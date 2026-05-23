using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class CourseMapperExtension
    {
        public static CourseResponse ToCourseResponse(Course course)
        {
            return new CourseResponse
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                SemesterId = course.SemesterId,
                SemesterName = course.Semester?.SemesterName ?? string.Empty
            };
        }

        public static List<CourseResponse> ToCourseResponseList(
           List<Course> courses)
        {
            return courses
                .Select(ToCourseResponse)
                .ToList();
        }
    }
}
