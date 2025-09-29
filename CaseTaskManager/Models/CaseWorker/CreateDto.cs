namespace CaseTaskManager.Models.CaseWorker
{
    public class CreateDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false; 

    }
}
