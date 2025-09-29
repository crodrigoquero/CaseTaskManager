// API project
namespace CaseTaskManager.Models.CaseWorker
{
    public class CaseWorkerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; }      
        public bool IsDeleted { get; set; }      
    }
}
