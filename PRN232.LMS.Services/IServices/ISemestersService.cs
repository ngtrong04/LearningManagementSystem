using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface ISemestersService
    {
        Task<ApiResponse<object>> GetAllAsync(QueryParameters query);
        Task<SemesterResponse> GetByIdAsync(int id);
        Task<ApiResponse<SemesterResponse>> CreateAsync(CreateSemesterRequest request);

        Task<ApiResponse<SemesterResponse>> UpdateAsync(int id, UpdateSemesterRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
