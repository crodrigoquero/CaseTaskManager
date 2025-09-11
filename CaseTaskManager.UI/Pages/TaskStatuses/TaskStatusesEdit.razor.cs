using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Pages.TaskStatuses;

public partial class TaskStatusesEdit : ComponentBase
{
    [Parameter] public int Id { get; set; }
    [Inject] private ITaskStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateCaseStatusDto? model;
    private bool isLoading = true;
    private bool saving;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        var existing = await Api.GetByIdAsync(Id);
        if (existing is not null)
        {
            model = new UpdateCaseStatusDto
            {
                StatusName = existing.StatusName
            };
        }
        isLoading = false;
    }

    private async Task HandleValidSubmit()
    {
        if (model is null) return;
        saving = true; error = null;

        var ok = await Api.UpdateAsync(Id, model);
        saving = false;

        if (ok) Nav.NavigateTo("/taskstatuses", forceLoad: true);
        else error = "Failed to update task status.";
    }

    private void Cancel() => Nav.NavigateTo("/taskstatuses");
}
