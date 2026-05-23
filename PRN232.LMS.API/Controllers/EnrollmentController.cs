using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Models.ResponseModel;
using PRN232.LMS.Services.IServices;

namespace PRN232.LMS.API.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<EnrollmentResponse>>>> GetAll(
            [FromQuery] QueryParameters request)
        {
            var result = await _enrollmentService.GetAllAsync(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentResponse>> GetById(int id)
        {
            var result = await _enrollmentService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    Message = $"Enrollment with id {id} not found"
                });
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<EnrollmentResponse>>> Create(
        [FromBody] CreateEnrollmentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<EnrollmentResponse>
                    {
                        success = false,
                        message = "Invalid request"
                    });
                }

                var result = await _enrollmentService.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.Data?.EnrollmentId },
                    result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<EnrollmentResponse>
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<EnrollmentResponse?>>> Update(
            int id,
            [FromBody] UpdateEnrollmentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponse<EnrollmentResponse?>
                    {
                        success = false,
                        message = "Invalid request"
                    });
                }

                var result = await _enrollmentService.UpdateAsync(id, request);

                if (!result.success)
                {
                    return NotFound(result);
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<EnrollmentResponse?>
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}
