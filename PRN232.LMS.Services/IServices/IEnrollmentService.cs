using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface IEnrollmentService
    {
        Task<ApiResponse<List<EnrollmentResponse>>> GetAllAsync(QueryParameters query);

        Task<EnrollmentResponse?> GetByIdAsync(int id);

        Task<ApiResponse<List<EnrollmentResponse>>> GetByCourseIdAsync(int courseId, bool expandStudent);
        Task<ApiResponse<EnrollmentResponse>> CreateAsync(
       CreateEnrollmentRequest request);

        Task<ApiResponse<EnrollmentResponse?>> UpdateAsync(
            int id,
            UpdateEnrollmentRequest request);
    }
}
