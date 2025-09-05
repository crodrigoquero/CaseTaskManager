using CaseTaskManager.Models.CaseStatus;

namespace CaseTaskManager.Interfaces
{
    public interface ICaseStatusService
    {
        Task<IEnumerable<CaseStatusDto>> GetAllCaseStatusesAsync();
        Task<CaseStatusDto?> GetCaseStatusByIdAsync(int id);
        Task<int> AddCaseStatusAsync(CaseStatusDto dto);
        Task<bool> UpdateCaseStatusAsync(int id, CaseStatusDto dto);
        Task<bool> DeleteCaseStatusAsync(int id);
    }
}
