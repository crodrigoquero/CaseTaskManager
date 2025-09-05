using System.ComponentModel.DataAnnotations;

namespace CaseTaskManager.UI.Models.Case
{
    public class CreateCaseDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        // Allow null if the API/SP accepts no initial status
        public int? CurrentStatusId { get; set; }
    }
}
