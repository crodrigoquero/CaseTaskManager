using System.ComponentModel.DataAnnotations;

namespace CaseTaskManager.UI.Models
{

    public class CreateCaseWorkerDto
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; } = string.Empty;

        // Optional; your API default is active=1. Keep it if you want to toggle at creation.
        public bool IsActive { get; set; } = true;
    }

}
