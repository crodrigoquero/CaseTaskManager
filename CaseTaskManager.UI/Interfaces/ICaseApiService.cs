using CaseTaskManager.UI.Models.Case;

namespace CaseTaskManager.UI.Interfaces
{
    public interface ICaseApiService
    {
        Task<List<CaseItem>> GetAllCasesAsync();

        Task<bool> CreateCaseAsync(CreateCaseDto newCase);

        // (optional if we want the new ID)
        Task<int?> CreateCaseAndReturnIdAsync(CreateCaseDto newCase);





        Task<CaseItem?> GetCaseByIdAsync(int id);

        Task<bool> UpdateCaseAsync(int id, UpdateCaseDetailsDto dto);
        Task<bool> DeleteCaseAsync(int id);


    }
}
