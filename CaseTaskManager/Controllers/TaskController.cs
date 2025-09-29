using CaseTaskManager.Interfaces;
using CaseTaskManager.Models.Task;
using CaseTaskManager.Models.TaskStatus;
using Microsoft.AspNetCore.Mvc;

namespace CaseTaskManager.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("create/task")]
        public async Task<IActionResult> CreateTask([FromBody] CreateDto taskDto)
        {
            var newTaskId = await _taskService.AddTaskAsync(taskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = newTaskId }, new { id = newTaskId });
        }


        [HttpGet("get/all/tasks")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAllTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("get/task/{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("get/all/tasks/from/case/{caseId}")]
        public async Task<IActionResult> GetTasksByCaseId(int caseId)
        {
            var tasks = await _taskService.GetTasksByCaseIdAsync(caseId);
            return Ok(tasks);
        }

        [HttpGet("get/all/task/of/type/{typeId}")]
        public async Task<IActionResult> GetTasksByTypeId(int typeId)
        {
            var tasks = await _taskService.GetTasksByTypeIdAsync(typeId);
            return Ok(tasks);
        }


        [HttpDelete("delete/task/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            return success ? Ok(new { deleted = true, id }) : NotFound();
        }



        [HttpPut("update/task/{id}/details")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Models.Task.UpdateDto taskDto)
        {
            var updated = await _taskService.UpdateTaskAsync(id, taskDto);
            if (!updated)
                return NotFound();

            return NoContent(); // 204
        }

        [HttpPatch("update/task/{TaskId}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int TaskId, [FromBody] Models.TaskStatus.UpdateDto dto)
        {
            var updated = await _taskService.UpdateTaskStatusAsync(TaskId, dto.StatusId);
            if (!updated)
                return NotFound();

            return NoContent();
        }


    }

}
