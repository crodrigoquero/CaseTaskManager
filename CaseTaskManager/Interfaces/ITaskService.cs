using CaseTaskManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaseTaskManager.Interfaces
{
    public interface ITaskService
    {
        // Tasks
        Task<int> AddTaskAsync(CreateTaskDto newTask);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByCaseIdAsync(int caseId);
        Task<IEnumerable<TaskItem>> GetTasksByTypeIdAsync(int typeId);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto taskDto);
        Task<bool> UpdateTaskStatusAsync(int taskId, int statusId);
        Task<bool> DeleteTaskAsync(int id);

        // Task Types
        Task<int> AddTaskTypeAsync(CreateTaskTypeDto taskTypeDto);
        Task<IEnumerable<TaskTypeDto>> GetAllTaskTypesAsync();
        Task<TaskTypeDto?> GetTaskTypeByIdAsync(int id);

        // Task Statuses
        Task<IEnumerable<TaskStatusDto>> GetAllTaskStatusesAsync();
        Task<TaskStatusDto?> GetTaskStatusByIdAsync(int id);   // <-- add this
        Task<TaskStatusDto?> AddTaskStatusAsync(string statusName);
        Task<bool> DeleteTaskStatusAsync(int id);
    }
}
