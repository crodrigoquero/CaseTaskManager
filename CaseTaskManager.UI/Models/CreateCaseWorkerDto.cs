using System.ComponentModel.DataAnnotations;

namespace CaseTaskManager.UI.Models
{

    public class CreateCaseWorkerDto
    {
        [Required, StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress, StringLength(255)]
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

}
