using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ICaseStatusApiService
    {
        Task<List<CaseStatusDto>> GetAllAsync();
        Task<CaseStatusDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCaseStatusDto newStatus);
        Task<bool> UpdateAsync(int id, UpdateCaseStatusDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
