using Microsoft.AspNetCore.Mvc;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Models;

namespace CaseTaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskStatusesController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskStatusesController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _taskService.GetAllTaskStatusesAsync();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _taskService.GetTaskStatusByIdAsync(id);
            if (status is null) return NotFound();
            return Ok(status);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] TaskStatusDto dto)
        {
            var created = await _taskService.AddTaskStatusAsync(dto.StatusName);
            return CreatedAtAction(nameof(GetAll), new { id = created?.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteTaskStatusAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
