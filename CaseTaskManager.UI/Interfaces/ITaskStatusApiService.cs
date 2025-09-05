using CaseTaskManager.UI.Models;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ITaskStatusApiService
    {
        Task<List<TaskStatusDto>> GetAllAsync();
        Task<TaskStatusDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCaseStatusDto newStatus);
        Task<bool> UpdateAsync(int id, UpdateCaseStatusDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
