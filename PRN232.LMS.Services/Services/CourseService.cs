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

namespace PRN232.LMS.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<CourseResponse>> CreateAsync(CreateCourseRequest request)
        {
            var createCourse = new Course
            {
                CourseId = request.Courseid,
                CourseName = request.CourseName,
                SemesterId = request.SemesterId
            };

            var res = await _unitOfWork.Courses.AddAsync(createCourse);
            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse<CourseResponse>
            {
                success = true,

                message = "Create course successfully",

                Data = CourseMapperExtension
                    .ToCourseResponse(res)
            };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(int id)
        {
            var course = await _unitOfWork.Courses.GetByIdAsync(id);
            if (course != null)
            {
                await _unitOfWork.Courses
                    .DeleteAsync(course.CourseId);

                await _unitOfWork.SaveChangesAsync();

                return new ApiResponse<bool>
                {
                    success = true,
                    message = "Delete course successfully"
                };
            }

            return new ApiResponse<bool>
            {
                success = false,
                message = "Delete course fails"
            };

        }

        public async Task<ApiResponse<List<CourseResponse>>> GetAllAsync(QueryParameters query)
        {
            var coursesQuery = _unitOfWork.Courses
              .GetQueryable()
              .Include(x => x.Semester);
            var totalItems = await coursesQuery.CountAsync();
            var courses = await coursesQuery
             .Skip((query.Page - 1) * query.Size)
             .Take(query.Size)
             .ToListAsync();
            var response =
              CourseMapperExtension
                  .ToCourseResponseList(courses);
            return new ApiResponse<List<CourseResponse>>
            {
                success = true,
                message = "Get courses successfully",
                Data = response,

                pagination = new PaginationMetadata
                {
                    Page = query.Page,
                    PageSize = query.Size,
                    TotalItems = totalItems,

                    TotalPages = (int)Math.Ceiling(
                (double)totalItems / query.Size)
                }
            };
        }

        public async Task<CourseResponse?> GetByIdAsync(int id)
        {
            var existingCourse = await _unitOfWork.Courses
                 .GetQueryable()
                 .Include(x => x.Semester)
                 .FirstOrDefaultAsync(
                     x => x.CourseId == id);

            if (existingCourse == null)
            {
                return null;
            }

            return CourseMapperExtension
                .ToCourseResponse(existingCourse);
        }

        public async Task<ApiResponse<CourseResponse>> UpdateAsync(int id, UpdateCourseRequest request)
        {
            var course =
                await _unitOfWork.Courses.GetByIdAsync(id);

            if (course == null)
            {
                return new ApiResponse<CourseResponse>
                {
                    success = false,
                    message = "Course not found"
                };
            }

            course.CourseName = request.CourseName;
            course.SemesterId = request.SemesterId;

            await _unitOfWork.Courses
                .UpdateAsync(course);

            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<CourseResponse>
            {
                success = true,

                message = "Update course successfully",

                Data = CourseMapperExtension
                   .ToCourseResponse(course)
            };
        }
    }
}
