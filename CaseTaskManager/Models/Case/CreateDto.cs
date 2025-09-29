// API project
namespace CaseTaskManager.Models.Case
{
    public class CreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CurrentStatusId { get; set; } 
    }
}
