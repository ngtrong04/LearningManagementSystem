using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.Entities;

namespace PRN232.LMS.Repositories.IRepositories
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        IStudentRepositories Students { get; }

        ISubjectRepositories Subjects { get; }

        ISemestersRepositories Semesters { get; }

        ICourseRepository Courses { get; }

        IEnrollmentRepositories Enrollments { get; }


    }
}
