using CaseTaskManager.Models.CaseWorker;

namespace CaseTaskManager.Interfaces
{
    public interface ICaseWorkerService
    {
        Task<CaseWorkerDto?> GetCaseWorkerByIdAsync(int id);
        Task<IEnumerable<CaseWorkerDto>> GetAllCaseWorkersAsync();
        Task<bool> UpdateCaseWorkerAsync(int id, UpdateCaseWorkerDto dto);
        Task<bool> ActivateCaseWorkerAsync(int caseWorkerId);
        Task<bool> DeactivateCaseWorkerAsync(int caseWorkerId);
        Task<bool> DeleteCaseWorkerAsync(int id);
        Task<int> AddCaseWorkerAsync(CreateCaseWorkerDto dto);

    }
}
