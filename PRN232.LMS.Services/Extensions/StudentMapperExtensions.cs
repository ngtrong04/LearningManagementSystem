using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Models.Entities;


namespace PRN232.LMS.Services.Extensions
{
    public static class StudentMapperExtensions
    {
        public static StudentResponse ToStudentResponse(
         this Student student)
        {
            return new StudentResponse
            {
                StudentId = student.StudentId,

                FullName = student.FullName,

                Email = student.Email,

                DateOfBirth = (DateTime)student.DateOfBirth,
                Enrollments = student.Enrollments.Select(x => new EnrollmentResponse
                {
                    EnrollmentId = x.EnrollmentId,
                    Status = x.Status
                }).ToList()
            };
        }
        public static List<StudentResponse> ToStudentResponseList(this IEnumerable<Student> students)
        {
            return students.Select(x => x.ToStudentResponse()).ToList();
        }
    }
}
