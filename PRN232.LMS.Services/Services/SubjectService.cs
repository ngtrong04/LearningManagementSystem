using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.Entities;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.Extensions;
using PRN232.LMS.Services.IServices;
using PRN232.LMS.Services.Utility;

namespace PRN232.LMS.Services.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubjectService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<SubjectResponse>> CreateAsync(CreateSubjectRequest request)
        {
            var createSubjectsRequest = new Subject
            {
                SubjectName = request.SubjectName,
                SubjectCode = request.SubjectCode,
                Credit = request.Credit
            };

            var res = await _unitOfWork.Subjects.AddAsync(createSubjectsRequest);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<SubjectResponse>
            {
                success = true,
                message = "Create subjects successfully",
                Data = SubjectMapperExtensions.ToSubjectResponse(res),
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var existingSubjects = await GetByIdAysnc(id);
            if (existingSubjects != null)
            {
                await _unitOfWork.Subjects.DeleteAsync(existingSubjects.SubjectId);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    success = true,
                    message = "Delete subjects successfully"
                };

            }
            return new ApiResponse<bool>
            {
                success = false,
                message = "Delete subjects Fails"

            };
        }

        public async Task<ApiResponse<IEnumerable<SubjectResponse>>> GetAllAsync(QueryParameters query)
        {
            IQueryable<Subject> subjectQuery = _unitOfWork.Subjects.GetQueryable();

            // Search 
            subjectQuery = SubjectQueryExtensions.Search(subjectQuery, query);

            // Sort 

            subjectQuery = SubjectQueryExtensions.Sort(subjectQuery, query);

            // Total Items
            int totalItems = await subjectQuery.CountAsync();


            subjectQuery = SubjectQueryExtensions.Paging(subjectQuery, query);


            List<Subject> subjects = await subjectQuery.ToListAsync();

            var response = SubjectMapperExtensions.ToSubjectResponseList(subjects);

            return new ApiResponse<IEnumerable<SubjectResponse>>
            {
                success = true,
                message = "Get subjects successfully",
                Data = response,
                pagination = new PaginationMetadata
                {
                    PageSize = query.Size,
                    Page = query.Page,
                    TotalItems = totalItems,
                    TotalPages = (int)Math.Ceiling((double)totalItems / query.Size),
                }
            };
        }

        public async Task<SubjectResponse> GetByIdAysnc(int id)
        {
            var existingSubjects = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (existingSubjects == null)
            {
                return null;
            }
            var response = SubjectMapperExtensions.ToSubjectResponse(existingSubjects);
            return response;
        }

        public async Task<ApiResponse<SubjectResponse>> UpdateAsync(int id, UpdateSubjectRequest request)
        {
            var existingSubjects = await _unitOfWork.Subjects.GetByIdAsync(id);
            if (existingSubjects == null)
            {
                return new ApiResponse<SubjectResponse>
                {
                    success = false,
                    message = "Subject Not Found"
                };
            }
            existingSubjects.SubjectCode = request.SubjectCode;
            existingSubjects.Credit = request.Credit;
            existingSubjects.SubjectName = request.SubjectName;

            await _unitOfWork.Subjects.UpdateAsync(existingSubjects);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<SubjectResponse>
            {
                success = true,
                message = "Update subjects successfully",
                Data = SubjectMapperExtensions.ToSubjectResponse(existingSubjects)
            };
        }
    }
}
