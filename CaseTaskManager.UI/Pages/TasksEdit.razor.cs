using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Tasks;

namespace CaseTaskManager.UI.Pages;

public partial class TasksEdit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateTaskDto? model;
    private bool isLoading = true;
    private bool saving;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        var existing = await Api.GetTaskByIdAsync(Id);
        if (existing is not null)
        {
            model = new UpdateTaskDto
            {
                Title = existing.Title,
                Description = existing.Description,
                StatusId = existing.StatusId,
                TaskTypeId = existing.TaskTypeId,
                DueDate = existing.DueDate
            };
        }
        isLoading = false;
    }

    private async Task HandleValidSubmit()
    {
        if (model is null) return;
        saving = true; error = null;

        var ok = await Api.UpdateTaskAsync(Id, model);
        saving = false;

        if (ok) Nav.NavigateTo("/tasks", forceLoad: true);
        else error = "Failed to update task.";
    }

    private void Cancel() => Nav.NavigateTo("/tasks");
}
