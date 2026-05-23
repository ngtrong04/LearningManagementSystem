using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Services.IServices;

namespace PRN232.LMS.API.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryParameters query)
        {
            var result = await _studentService.GetAllAsync(query);
            return Ok(result);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _studentService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new { message = $"Student with id {id} not found" });
            }
            return Ok(result);

        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStudentRequest request)
        {
            var result = await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new
            {
                id = result.Data.StudentId
            }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateStudentRequest request)
        {
            await _studentService.UpdateAsync(id, request);

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentService.DeleteAsync(id);

            return NoContent();
        }
    }
}
