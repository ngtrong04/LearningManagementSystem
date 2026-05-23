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

        public UnitOfWork(
            LmsDbContext context,
            IStudentRepositories students,
            ISubjectRepositories subjects)
        {
            _context = context;

            Students = students;

            Subjects = subjects;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
