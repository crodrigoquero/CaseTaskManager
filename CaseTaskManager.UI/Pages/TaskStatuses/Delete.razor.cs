using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.TaskStatus;

namespace CaseTaskManager.UI.Pages.TaskStatuses;

public partial class Delete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private TaskStatusDto? item;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        // Be tolerant of 404s in the service to avoid exceptions here
        item = await Api.GetByIdAsync(Id);
        isLoading = false;
    }

    private async Task ConfirmDelete()
    {
        if (item is null) return;

        var ok = await Api.DeleteAsync(Id);
        if (ok)
            Nav.NavigateTo("/taskstatuses", forceLoad: true);
        // else: optionally show an error/alert
    }

    private void Cancel() => Nav.NavigateTo("/taskstatuses");
}
