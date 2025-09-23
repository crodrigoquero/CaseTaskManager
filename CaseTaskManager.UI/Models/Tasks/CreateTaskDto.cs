// CaseTaskManager.UI/Models/Tasks/CreateTaskDto.cs
using System.ComponentModel.DataAnnotations;

public class CreateTaskDto
{
    [Required(ErrorMessage = "Please select a case")]
    public int? CaseId { get; set; }            

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    [Required(ErrorMessage = "Please select a status")]
    public int? StatusId { get; set; }

    [Required(ErrorMessage = "Please select a task type")]
    public int? TaskTypeId { get; set; }

    [Required]
    public DateTime DueDate { get; set; } = DateTime.Today;
}
