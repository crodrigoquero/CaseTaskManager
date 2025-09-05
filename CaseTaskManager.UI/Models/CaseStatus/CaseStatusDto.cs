namespace CaseTaskManager.UI.Models.CaseStatus
{
    public class CaseStatusDto
    {
        public int Id { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
