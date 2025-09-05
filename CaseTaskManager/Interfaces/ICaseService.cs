using CaseTaskManager.Models;
using CaseTaskManager.Models.Case;

namespace CaseTaskManager.Interfaces
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseDto>> GetAllCasesAsync();
        Task<CaseDto?> GetCaseByIdAsync(int id);
        Task<bool> UpdateCaseDetailsAsync(int id, UpdateCaseDetailsDto dto);
        Task<bool> UpdateCaseStatusAsync(int caseId, int statusId);
        Task<bool> DeleteCaseAsync(int id);
        Task<int> AddCaseAsync(CreateCaseDto dto);
        Task<int> AssignCaseWorkerAsync(CaseAssignmentDto assignmentDto);
        Task<bool> RemoveCaseAssignmentAsync(CaseAssignmentDto assignmentDto);



    }
}
