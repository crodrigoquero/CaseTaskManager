using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Tasks;
using CaseTaskManager.UI.Models.TaskStatus; // TaskStatusDto (Id, StatusName)
using CaseTaskManager.UI.Models.TaskType;   // TaskTypeDto (Id, TypeName)

namespace CaseTaskManager.UI.Pages.Tasks;

public partial class Edit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskApiService Api { get; set; } = default!;
    [Inject] private ITaskStatusApiService StatusApi { get; set; } = default!;
    [Inject] private ITaskTypeApiService TypeApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateTaskDto? model;
    private List<TaskStatusDto> taskStatuses = new();
    private List<TaskTypeDto> taskTypes = new();
    private bool isLoading = true;
    private bool saving;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            // load dropdowns + current task in parallel
            var statusTask = StatusApi.GetAllAsync();
            var typeTask = TypeApi.GetAllAsync();
            var taskTask = Api.GetTaskByIdAsync(Id);

            await Task.WhenAll(statusTask, typeTask, taskTask);

            taskStatuses = statusTask.Result?.ToList() ?? new();
            taskTypes = typeTask.Result?.ToList() ?? new();

            var existing = taskTask.Result;
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
        }
        finally
        {
            isLoading = false;
        }
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
