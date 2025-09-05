using CaseTaskManager.Interfaces;
using CaseTaskManager.Models;
using CaseTaskManager.Models.CaseWorker;
using Microsoft.AspNetCore.Mvc;

namespace CaseTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseWorkersController : ControllerBase
    {
        private readonly ICaseWorkerService _caseWorkerService;

        public CaseWorkersController(ICaseWorkerService caseWorkerService)
        {
            _caseWorkerService = caseWorkerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCaseWorkerById(int id)
        {
            var worker = await _caseWorkerService.GetCaseWorkerByIdAsync(id);
            if (worker == null) return NotFound();
            return Ok(worker);
        }

        [HttpGet("get/all/caseworkers")]
        public async Task<ActionResult<IEnumerable<CaseWorkerDto>>> GetAll()
        {
            var workers = await _caseWorkerService.GetAllCaseWorkersAsync();
            return Ok(workers);
        }

        [HttpPut("update/caseworker/{id}")]
        public async Task<IActionResult> UpdateCaseWorker(int id, [FromBody] UpdateCaseWorkerDto dto)
        {
            var success = await _caseWorkerService.UpdateCaseWorkerAsync(id, dto);
            if (!success) return NotFound();

            return NoContent(); // 204
        }

        [HttpPatch("activate/caseworker/{id}")]
        public async Task<IActionResult> ActivateCaseWorker(int id)
        {
            var success = await _caseWorkerService.ActivateCaseWorkerAsync(id);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPatch("deactivate/caseworker/{id}")]
        public async Task<IActionResult> DeactivateCaseWorker(int id)
        {
            var success = await _caseWorkerService.DeactivateCaseWorkerAsync(id);
            if (!success) return NotFound();

            return NoContent();   // 204 on success
        }

        [HttpDelete("delete/caseworker/{id}")]
        public async Task<IActionResult> DeleteCaseWorker(int id)
        {
            var success = await _caseWorkerService.DeleteCaseWorkerAsync(id);
            if (!success) return NotFound();

            return NoContent(); // 204
        }

        [HttpPost("create/caseworker")]
        public async Task<IActionResult> CreateCaseWorker([FromBody] CreateCaseWorkerDto dto)
        {
            var newId = await _caseWorkerService.AddCaseWorkerAsync(dto);
            return CreatedAtAction(nameof(GetCaseWorkerById), new { id = newId }, new { id = newId });
        }


    }
}

