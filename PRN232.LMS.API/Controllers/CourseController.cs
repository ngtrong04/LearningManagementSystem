using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Services.IServices;

namespace PRN232.LMS.API.Controllers
{
    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] QueryParameters request)
        {
            var result =
                await _courseService.GetAllAsync(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result =
                await _courseService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Course not found"
                });
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateCourseRequest request)
        {
            var result =
                await _courseService.CreateAsync(request);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] UpdateCourseRequest request)
        {
            var result =
                await _courseService.UpdateAsync(
                    id,
                    request);

            if (!result.success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result =
                await _courseService.DeleteAsync(id);

            if (!result.success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
