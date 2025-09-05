namespace CaseTaskManager.Models.CaseWorker
{
    public class CreateCaseWorkerDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false; // Optional, default is false

    }
}
