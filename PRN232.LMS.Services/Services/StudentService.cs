using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Repositories.IRepositories;
using PRN232.LMS.Services.Extensions;
using PRN232.LMS.Services.IServices;
using PRN232.LMS.Services.Utility;

namespace PRN232.LMS.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<StudentResponse>> CreateAsync(CreateStudentRequest request)
        {
            var createStudentRequest = new Models.Entities.Student
            {
                DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Unspecified),
                Email = request.Email,
                FullName = request.FullName

            };
            var res = await _unitOfWork.Students.AddAsync(createStudentRequest);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<
     StudentResponse>
            {
                success = true,

                message = "Create student successfully",

                Data = StudentMapperExtensions.ToStudentResponse(res)
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if (student != null)
            {
                await _unitOfWork.Students.DeleteAsync(student.StudentId);
                await _unitOfWork.SaveChangesAsync();
                return new ApiResponse<bool>
                {
                    success = true,
                    message = "Delete student successfully"
                };

            }
            return new ApiResponse<bool>
            {
                success = false,
                message = "Delete student Fails"
            };
        }

        public async Task<ApiResponse<List<StudentResponse>>> GetAllAsync(QueryParameters query)
        {
            var studentsQuery = _unitOfWork.Students.GetQueryable();

            // Search 
            studentsQuery = StudentQueryExtensions.Search(studentsQuery, query);
            // Sort
            studentsQuery = StudentQueryExtensions.Sort(studentsQuery, query);

            // Expand
            studentsQuery = StudentQueryExtensions.Expand(studentsQuery, query);

            // TOTAL ITEMS
            var totalItems = await studentsQuery.CountAsync();

            // Pading
            studentsQuery = StudentQueryExtensions.Paging(studentsQuery, query);

            var students = await studentsQuery.ToListAsync();

            var response = StudentMapperExtensions.ToStudentResponseList(students);

            return new ApiResponse<List<StudentResponse>>
            {
                success = true,
                message = "Get students successfully",
                Data = response,
                pagination = new PaginationMetadata
                {
                    Page = query.Page,
                    PageSize = query.Size,
                    TotalItems = totalItems,
                    TotalPages =
                            (int)Math.Ceiling(
                                (double)totalItems
                                / query.Size)
                }
            };
        }

        public async Task<StudentResponse> GetByIdAsync(int id)
        {
            var existingStudents = await _unitOfWork.Students.GetByIdAsync(id);
            if (existingStudents == null)
            {
                return null;
            }
            var toStudentResponse = StudentMapperExtensions.ToStudentResponse(existingStudents);
            return toStudentResponse;
        }

        public async Task<ApiResponse<StudentResponse>> UpdateAsync(int id, UpdateStudentRequest request)
        {
            var student =
                  await _unitOfWork
                      .Students
                      .GetByIdAsync(id);
            if (student == null)
            {
                return new ApiResponse<StudentResponse>
                {
                    success = false,

                    message =
                "Student not found"
                };
            }
            student.Email = request.Email;
            student.DateOfBirth = DateTime.SpecifyKind(request.DateOfBirth, DateTimeKind.Unspecified);
            student.FullName = request.FullName;

            await _unitOfWork.Students.UpdateAsync(student);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<
        StudentResponse>
            {
                success = true,

                message =
            "Update student successfully",

                Data =
            student.ToStudentResponse()
            };
        }
    }
}
