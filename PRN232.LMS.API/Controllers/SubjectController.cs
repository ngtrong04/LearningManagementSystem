using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRN232.LMS.Models.RequestModel;
using PRN232.LMS.Services.IServices;

namespace PRN232.LMS.API.Controllers
{
    [Route("api/subjects")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] QueryParameters query)
        {
            var result = await _subjectService.GetAllAsync(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _subjectService.GetByIdAysnc(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateSubjectRequest request)
        {
            var result = await _subjectService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new
            {
                id = result.Data.SubjectId
            }, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateSubjectRequest request)
        {
            await _subjectService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _subjectService.DeleteAsync(id);
            return NoContent();
        }
    }
}
