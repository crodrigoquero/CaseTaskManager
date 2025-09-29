using CaseTaskManager.UI.Models.CaseWorker;

public interface ICaseWorkerApiService
{
    Task<List<CaseWorkerDto>> GetAllAsync();
    Task<CaseWorkerDto?> GetByIdAsync(int id);
    Task<int> AddAsync(CaseWorkerDto caseWorker);
    Task<bool> UpdateAsync(int id, UpdateCaseWorkerDto dto); 
    Task<bool> ActivateAsync(int id);
    Task<bool> DeactivateAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<bool> CreateAsync(CreateCaseWorkerDto dto);
}
