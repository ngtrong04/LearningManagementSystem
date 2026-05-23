using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;

namespace PRN232.LMS.Services.Utility
{
    public static class StudentQueryExtensions
    {
        public static IQueryable<Student> Search(this IQueryable<Student> query, QueryParameters request)
        {
            if (string.IsNullOrEmpty(request.Search))
            {
                return query;
            }
            var search = request.Search.ToLower();

            return query.Where(x => x.FullName.ToLower().Contains(search));
        }


        public static IQueryable<Student> Sort(this IQueryable<Student> query, QueryParameters request)
        {
            return request.Sort switch
            {
                "fullName" => query.OrderBy(
                        x => x.FullName),

                "-fullName" => query.OrderByDescending(
                            x => x.FullName),
                "dateOfBirth" => query.OrderBy(x => x.DateOfBirth),

                "-dateOfBirth" => query.OrderByDescending(x => x.DateOfBirth),
                _ => query.OrderBy(x => x.StudentId)
            };
        }

        public static IQueryable<Student> Paging(
            this IQueryable<Student> query,
            QueryParameters request)
        {
            return query.Skip((request.Page - 1) * request.Size).Take(request.Size);
        }


        public static IQueryable<Student> Expand(this IQueryable<Student> query, QueryParameters request)
        {
            if (string.IsNullOrEmpty(request.Expand))
            {
                return query;
            }
            var expands = request.Expand.Split(",");
            foreach (var item in expands)
            {
                switch (item.ToLower())
                {
                    case "enrollments":
                        query = query.Include(x => x.Enrollments);
                        break;
                }
            }
            return query;
        }
    }
}
