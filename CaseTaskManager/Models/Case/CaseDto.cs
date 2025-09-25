namespace CaseTaskManager.Models.Case
{
    public class CaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int? CurrentStatusId { get; set; }
        public string? CurrentStatusName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
