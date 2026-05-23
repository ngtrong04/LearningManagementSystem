using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface IStudentService
    {
        Task<ApiResponse<List<StudentResponse>>> GetAllAsync(QueryParameters query);
        Task<StudentResponse> GetByIdAsync(int id);
        Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request);

        Task<ApiResponse<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
