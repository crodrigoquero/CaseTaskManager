using CaseTaskManager.Models.Task;
using CaseTaskManager.Models.TaskStatus;
using CaseTaskManager.Models.TaskType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTaskManager.Interfaces
{
    public interface ITaskService
    {
        // Tasks
        Task<int> AddTaskAsync(Models.Task.CreateDto newTask);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByCaseIdAsync(int caseId);
        Task<IEnumerable<TaskItem>> GetTasksByTypeIdAsync(int typeId);
        Task<bool> UpdateTaskAsync(int id, Models.Task.UpdateDto taskDto);
        Task<bool> UpdateTaskStatusAsync(int taskId, int statusId);
        Task<bool> DeleteTaskAsync(int id);

        // Task Types
        Task<int> AddTaskTypeAsync(Models.TaskType.CreateDto taskTypeDto);
        Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync();
        Task<TaskTypeDto?> GetTaskTypeByIdAsync(int id);

        // Task Statuses
        Task<IEnumerable<TaskStatusDto>> GetAllTaskStatusesAsync();
        Task<TaskStatusDto?> GetTaskStatusByIdAsync(int id);   // <-- add this
        Task<TaskStatusDto?> AddTaskStatusAsync(string statusName);
        Task<bool> DeleteTaskStatusAsync(int id);
    }
}
