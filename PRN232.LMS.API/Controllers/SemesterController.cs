using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Services.IServices;

namespace PRN232.LMS.API.Controllers
{
    [Route("api/semesters")]
    [ApiController]
    public class SemesterController : ControllerBase
    {
        private readonly ISemestersService _semesterService;

        public SemesterController(ISemestersService semestersService)
        {
            _semesterService = semestersService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(
            [FromQuery] QueryParameters request)
        {
            var result =
                await _semesterService.GetAllAsync(request);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result =
                await _semesterService.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateSemesterRequest request)
        {
            var result =
                await _semesterService.CreateAsync(request);

            return CreatedAtAction(nameof(GetById), new
            {
                id = result.Data.SemesterId
            }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] UpdateSemesterRequest request)
        {
            var result =
                await _semesterService.UpdateAsync(id, request);

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
                await _semesterService.DeleteAsync(id);

            if (!result.success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}
