using System.ComponentModel.DataAnnotations;

namespace CaseTaskManager.UI.Models
{
    public class CreateTaskTypeDto
    {
        [Required, StringLength(100)]
        public string TypeName { get; set; } = string.Empty;

        [StringLength(255)]
        public string? Description { get; set; }
    }
}
