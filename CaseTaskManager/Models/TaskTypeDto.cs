namespace CaseTaskManager.Models
{
    public class TaskTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
