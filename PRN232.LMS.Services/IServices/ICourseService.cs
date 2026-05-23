using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface ICourseService
    {
        Task<ApiResponse<List<CourseResponse>>> GetAllAsync(
            QueryParameters query);

        Task<CourseResponse?> GetByIdAsync(int id);

        Task<ApiResponse<CourseResponse>> CreateAsync(
            CreateCourseRequest request);

        Task<ApiResponse<CourseResponse>> UpdateAsync(
            int id,
            UpdateCourseRequest request);

        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
