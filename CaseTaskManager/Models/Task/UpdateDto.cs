namespace CaseTaskManager.Models.Task
{
    public class UpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StatusId { get; set; }
        public int TaskTypeId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
