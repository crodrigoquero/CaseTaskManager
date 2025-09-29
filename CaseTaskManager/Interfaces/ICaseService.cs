using CaseTaskManager.Models;
using CaseTaskManager.Models.Case;
using CaseTaskManager.Models.CaseAssignment;

namespace CaseTaskManager.Interfaces
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseDto>> GetAllCasesAsync();
        Task<CaseDto?> GetCaseByIdAsync(int id);
        Task<bool> UpdateCaseDetailsAsync(int id, UpdateDto dto);
        Task<bool> UpdateCaseStatusAsync(int caseId, int statusId);
        Task<bool> DeleteCaseAsync(int id);
        Task<int> AddCaseAsync(CreateDto dto);
        Task<int> AssignCaseWorkerAsync(CaseAssignmentDto assignmentDto);
        Task<bool> RemoveCaseAssignmentAsync(CaseAssignmentDto assignmentDto);



    }
}
