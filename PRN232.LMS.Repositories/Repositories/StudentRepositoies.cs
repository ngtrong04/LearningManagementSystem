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
    public class StudentRepositoies : GenericRepositories<Student>, IStudentRepositories
    {
        public StudentRepositoies(LmsDbContext lmsdbContext) : base(lmsdbContext)
        {
        }
    }
}
