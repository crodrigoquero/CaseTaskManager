using Microsoft.AspNetCore.Mvc;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models;
using CaseTaskManager.Models.Case;
using Microsoft.Data.SqlClient;


namespace CaseTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CasesController : ControllerBase
    {
        private readonly ICaseService _caseService;

        public CasesController(ICaseService caseService)
        {
            _caseService = caseService;
        }


        [HttpGet("get/all/cases")]
        public async Task<IActionResult> GetAllCases()
        {
            var cases = await _caseService.GetAllCasesAsync();
            return Ok(cases);
        }

        [HttpGet("get/case/{id}")]
        public async Task<IActionResult> GetCaseById(int id)
        {
            var result = await _caseService.GetCaseByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCase(int id, [FromBody] UpdateCaseDetailsDto dto)
        {
            var updated = await _caseService.UpdateCaseDetailsAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent(); // 204 success with no content
        }

        [HttpPost("create/case")]
        public async Task<IActionResult> CreateCase([FromBody] CreateCaseDto dto)
        {
            if (dto is null) return BadRequest("Payload required.");

            // Make sure your ICaseService exposes AddCaseAsync(CreateCaseDto) -> int (new Id)
            var newId = await _caseService.AddCaseAsync(dto);

            // Return 201 + location header + body with the new id
            return CreatedAtAction(nameof(GetCaseById), new { id = newId }, new { id = newId });
        }

        [HttpPatch("update/case/{caseId}/status")]
        public async Task<IActionResult> UpdateCaseStatus(int caseId, [FromBody] UpdateCaseStatusDto dto)
        {
            var success = await _caseService.UpdateCaseStatusAsync(caseId, dto.StatusId);
            if (!success)
                return NotFound();

            return NoContent(); // 204 success
        }

        [HttpDelete("delete/case/{id}")]
        public async Task<IActionResult> DeleteCase(int id)
        {
            var success = await _caseService.DeleteCaseAsync(id);
            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpPost("assign/case")]
        public async Task<IActionResult> AssignCaseWorker([FromBody] CaseAssignmentDto assignmentDto)
        {
            try
            {
                var assignmentId = await _caseService.AssignCaseWorkerAsync(assignmentDto);
                return Ok(new { assignmentId });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPatch("unassign/case")]
        public async Task<IActionResult> UnassignCaseWorker([FromBody] CaseAssignmentDto assignmentDto)
        {
            var result = await _caseService.RemoveCaseAssignmentAsync(assignmentDto);
            if (!result)
                return NotFound("Assignment not found or already removed.");

            return NoContent();
        }

    }
}
