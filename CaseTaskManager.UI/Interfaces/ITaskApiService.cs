using CaseTaskManager.UI.Models;
using CaseTaskManager.UI.Models.Tasks;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ITaskApiService
    {
        Task<List<TaskItem>> GetAllTasksAsync();
        Task<bool> CreateTaskAsync(CreateTaskDto newTask);
        Task<bool> DeleteTaskAsync(int id);
        Task<bool> UpdateTaskStatusAsync(int id, int statusId);
        Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto);
        Task<TaskItem?> GetTaskByIdAsync(int id);

    }
}
