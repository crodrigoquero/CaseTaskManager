using Microsoft.AspNetCore.Mvc;
using CaseTaskManager.Interfaces;
using CaseTaskManager.Services;
using CaseTaskManager.Models.TaskType;


namespace CaseTaskManager.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TaskTypesController : ControllerBase
    {
        private readonly ITaskService _taskTypeService;

        public TaskTypesController(ITaskService taskTypeService)
        {
            _taskTypeService = taskTypeService;
        }

        [HttpGet("get/all/tasktypes")]
        public async Task<IActionResult> GetAllTaskTypes()
        {
            var taskTypes = await _taskTypeService.GetAllTaskTypesAsync();
            return Ok(taskTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskTypeById(int id)
        {
            var type = await _taskTypeService.GetTaskTypeByIdAsync(id);
            if (type == null)
                return NotFound();

            return Ok(type);
        }

        [HttpPost("create/task/type")]
        public async Task<IActionResult> CreateTaskType([FromBody] CreateDto taskTypeDto)
        {
            var newId = await _taskTypeService.AddTaskTypeAsync(taskTypeDto);
            return CreatedAtAction(nameof(GetTaskTypeById), new { id = newId }, new { id = newId });
        }

    }
}


