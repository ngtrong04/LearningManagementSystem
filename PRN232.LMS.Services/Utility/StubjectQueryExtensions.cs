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
    public static class StubjectQueryExtensions
    {
        public static IQueryable<Subject> Search(this IQueryable<Subject> query, QueryParameters request)
        {
            if (string.IsNullOrEmpty(request.Search))
            {
                return query;
            }
            var search = query.Where(x => x.SubjectName.ToLower().Contains(request.Search.ToLower()));
            return search;
        }

        public static IQueryable<Subject> Sort(this IQueryable<Subject> query, QueryParameters request)
        {
            return request.Sort switch
            {
                "subjectName" => query.OrderBy(x => x.SubjectName),
                "-subjectName" => query.OrderByDescending(x => x.SubjectName),
                _ => query.OrderBy(x => x.SubjectId)
            };
        }

        public static IQueryable<Subject> Paging(
            this IQueryable<Subject> query,
            QueryParameters request)
        {
            return query.Skip((request.Page - 1) * request.Size).Take(request.Size);
        }
    }
}
