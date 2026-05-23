using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Repositories.Data;
using PRN232.LMS.Repositories.IRepositories;

namespace PRN232.LMS.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LmsDbContext _context;
        public IStudentRepositories Students { get; }

        public ISubjectRepositories Subjects { get; }

        public ISemestersRepositories Semesters { get; }

        public ICourseRepository Courses { get; }

        public IEnrollmentRepositories Enrollments { get; }

        public UnitOfWork(
            LmsDbContext context,
            IStudentRepositories students,
            ISubjectRepositories subjects,
            ISemestersRepositories semesters,
            ICourseRepository courses,
            IEnrollmentRepositories enrollments
            )
        {
            _context = context;
            Students = students;
            Subjects = subjects;
            Semesters = semesters;
            Courses = courses;
            Enrollments = enrollments;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
