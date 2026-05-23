using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Utility
{
    public static class EnrollmentExtensions
    {
        public static IQueryable<Enrollment> Search(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            if (string.IsNullOrEmpty(request.Search))
            {
                return query;
            }

            var search = request.Search.ToLower();

            return query.Where(x =>
                x.Student.FullName.ToLower().Contains(search) ||
                x.Course.CourseName.ToLower().Contains(search) ||
                x.Status.ToLower().Contains(search));
        }

        public static IQueryable<Enrollment> Sort(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            return request.Sort switch
            {
                "studentName" => query.OrderBy(
                    x => x.Student.FullName),

                "-studentName" => query.OrderByDescending(
                    x => x.Student.FullName),

                "courseName" => query.OrderBy(
                    x => x.Course.CourseName),

                "-courseName" => query.OrderByDescending(
                    x => x.Course.CourseName),

                "enrollDate" => query.OrderBy(
                    x => x.EnrollDate),

                "-enrollDate" => query.OrderByDescending(
                    x => x.EnrollDate),

                "status" => query.OrderBy(
                    x => x.Status),

                "-status" => query.OrderByDescending(
                    x => x.Status),

                _ => query.OrderBy(x => x.EnrollmentId)
            };
        }

        public static IQueryable<Enrollment> Paging(
            this IQueryable<Enrollment> query,
            QueryParameters request)
        {
            return query.Skip((request.Page - 1) * request.Size)
                        .Take(request.Size);
        }


    }
}
