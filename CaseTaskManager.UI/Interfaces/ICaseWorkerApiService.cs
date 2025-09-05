using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ICaseWorkerApiService
    {
        Task<List<CaseWorkerDto>> GetAllAsync();
        Task<CaseWorkerDto?> GetByIdAsync(int id);
        Task<int> AddAsync(CaseWorkerDto caseWorker);
        Task<bool> UpdateAsync(int id, CaseWorkerDto caseWorker);
        Task<bool> ActivateAsync(int id);
        Task<bool> DeactivateAsync(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> CreateAsync(CreateCaseWorkerDto dto);
    }
}
