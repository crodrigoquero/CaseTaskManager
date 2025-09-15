namespace CaseTaskManager.UI.Models.TaskType
{
    public class TaskTypeDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
