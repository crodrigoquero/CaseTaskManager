using Microsoft.AspNetCore.Mvc;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models.CaseStatus;

namespace CaseTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseStatusesController : ControllerBase
    {
        private readonly ICaseStatusService _service;

        public CaseStatusesController(ICaseStatusService service)
        {
            _service = service;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAllCaseStatuses()
        {
            var statuses = await _service.GetAllCaseStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaseStatusById(int id)
        {
            var status = await _service.GetCaseStatusByIdAsync(id);
            if (status == null)
                return NotFound();
            return Ok(status);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCaseStatus([FromBody] CaseStatusDto dto)
        {
            var newId = await _service.AddCaseStatusAsync(dto);
            return CreatedAtAction(nameof(GetCaseStatusById), new { id = newId }, new { id = newId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCaseStatus(int id, [FromBody] CaseStatusDto dto)
        {
            var updated = await _service.UpdateCaseStatusAsync(id, dto);
            if (!updated)
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCaseStatus(int id)
        {
            var deleted = await _service.DeleteCaseStatusAsync(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }
    }
}
