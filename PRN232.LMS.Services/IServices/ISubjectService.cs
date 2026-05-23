using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;

namespace PRN232.LMS.Services.IServices
{
    public interface ISubjectService
    {
        Task<ApiResponse<IEnumerable<SubjectResponse>>> GetAllAsync(QueryParameters query);
        Task<SubjectResponse> GetByIdAysnc(int id);
        Task<ApiResponse<SubjectResponse>> CreateAsync(CreateSubjectRequest request);
        Task<ApiResponse<SubjectResponse>> UpdateAsync(int id, UpdateSubjectRequest request);
        Task<ApiResponse<bool>> DeleteAsync(int id);
    }
}
