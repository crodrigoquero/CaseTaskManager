namespace CaseTaskManager.UI.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CaseId { get; set; }
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
