namespace CaseTaskManager.Models
{
    public sealed class TaskItem
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string? CaseTitle { get; set; }  
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int StatusId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? StatusName { get; set; }
        public string? TaskTypeName { get; set; }
    }
}
