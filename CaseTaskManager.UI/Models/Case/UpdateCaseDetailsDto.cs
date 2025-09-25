using System.ComponentModel.DataAnnotations;

namespace CaseTaskManager.UI.Models.Case
{
    public class UpdateCaseDetailsDto
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Please select a case status")]
        public int? StatusId { get; set; }
    }
}
