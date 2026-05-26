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
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOFWork;

        public EnrollmentService(IUnitOfWork unitOFWork)
        {
            _unitOFWork = unitOFWork;
        }

        public async Task<ApiResponse<EnrollmentResponse>> CreateAsync(CreateEnrollmentRequest request)
        {
            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                EnrollDate = DateTime.SpecifyKind(request.EnrollDate, DateTimeKind.Utc),
                Status = request.Status,
                EnrollmentId = request.Enrollmentid
            };
            await _unitOFWork.Enrollments.AddAsync(enrollment);
            await _unitOFWork.SaveChangesAsync();

            enrollment = await _unitOFWork.Enrollments.GetQueryable().Include(x => x.Student).Include(x => x.Course).FirstAsync(x => x.EnrollmentId == enrollment.EnrollmentId);
            return new ApiResponse<EnrollmentResponse>
            {
                success = true,
                message = "Create enrollment successfully",
                Data = enrollment.ToEnrollmentResponse()
            };
        }

        public async Task<ApiResponse<List<EnrollmentResponse>>> GetAllAsync(QueryParameters query)
        {
            var enrollermentQuery = _unitOFWork.Enrollments.GetQueryable().Include(x => x.Student).Include(x => x.Course).Search(query).Sort(query).Paging(query);
            var totalItems = await enrollermentQuery.CountAsync();
            var enrollments = await enrollermentQuery.ToListAsync();
            var result = enrollments.ToEnrollmentResponseList();
            return new ApiResponse<List<EnrollmentResponse>>
            {
                success = true,
                message = "Get enrollments successfully",
                Data = result,
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

        public async Task<EnrollmentResponse?> GetByIdAsync(int id)
        {
            var enrollment = await _unitOFWork.Enrollments.GetQueryable()
                .Include(x => x.Student)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.EnrollmentId == id);

            if (enrollment == null)
            {
                return null;
            }

            return enrollment.ToEnrollmentResponse();
        }

        public async Task<ApiResponse<List<EnrollmentResponse>>> GetByCourseIdAsync(int courseId, bool expandStudent)
        {
            // Kiểm tra course có tồn tại không
            var courseExists = await _unitOFWork.Courses.GetQueryable()
                .AnyAsync(x => x.CourseId == courseId);

            if (!courseExists)
            {
                return new ApiResponse<List<EnrollmentResponse>>
                {
                    success = false,
                    message = $"Course with id {courseId} not found",
                    Data = null
                };
            }

            // Build query enrollments theo courseId
            IQueryable<Enrollment> query = _unitOFWork.Enrollments.GetQueryable()
                .Where(x => x.CourseId == courseId)
                .Include(x => x.Course);

            // Nếu có ?expand=student thì Include thêm Student
            if (expandStudent)
            {
                query = query.Include(x => x.Student);
            }

            var enrollments = await query.ToListAsync();

            return new ApiResponse<List<EnrollmentResponse>>
            {
                success = true,
                message = $"Get enrollments for course {courseId} successfully",
                Data = enrollments.ToEnrollmentResponseList()
            };
        }

        public async Task<ApiResponse<EnrollmentResponse?>> UpdateAsync(int id, UpdateEnrollmentRequest request)
        {
            var enrollment = await _unitOFWork.Enrollments.GetQueryable().FirstOrDefaultAsync(x => x.EnrollmentId == id);

            if (enrollment == null)
            {
                return new ApiResponse<EnrollmentResponse?>
                {
                    success = false,
                    message = $"Enrollment with id {id} not found",
                    Data = null
                };
            }

            enrollment.StudentId = request.StudentId;
            enrollment.CourseId = request.CourseId;
            enrollment.EnrollDate = request.EnrollDate;
            enrollment.Status = request.Status;

            await _unitOFWork.Enrollments.UpdateAsync(enrollment);
            await _unitOFWork.SaveChangesAsync();

            enrollment = await _unitOFWork.Enrollments.GetQueryable()
      .Include(x => x.Student)
      .Include(x => x.Course)
      .FirstAsync(x => x.EnrollmentId == id);
            return new ApiResponse<EnrollmentResponse?>
            {
                success = true,
                message = "Update enrollment successfully",
                Data = enrollment.ToEnrollmentResponse()
            };
        }

    }
}
