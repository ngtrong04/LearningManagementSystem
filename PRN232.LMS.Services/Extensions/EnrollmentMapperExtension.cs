using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.Extensions
{
    public static class EnrollmentMapperExtension
    {
        public static EnrollmentResponse ToEnrollmentResponse(this Enrollment enrollment)
        {
            return new EnrollmentResponse
            {
                EnrollmentId = enrollment.EnrollmentId,
                EnrollDate = enrollment.EnrollDate,
                Status = enrollment.Status,
                StudentId = enrollment.Student.StudentId,
                StudentName = enrollment.Student.FullName,
                StudentEmail = enrollment.Student.Email,
                CourseId = enrollment.Course.CourseId,
                CourseName = enrollment.Course.CourseName
            };
        }

        public static List<EnrollmentResponse> ToEnrollmentResponseList(
      this List<Enrollment> enrollments)
        {
            return enrollments.Select(x => x.ToEnrollmentResponse())
                              .ToList();
        }
    }
}
