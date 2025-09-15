using CaseTaskManager.UI.Models.TaskType;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ITaskTypeApiService
    {
        Task<List<TaskTypeDto>> GetAllAsync();
        Task<TaskTypeDto?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, UpdateTaskTypeDto dto);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(CreateTaskTypeDto dto);
    }
}
