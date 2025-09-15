using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.TaskType;

namespace CaseTaskManager.UI.Pages.TaskTypes;

public partial class Delete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskTypeApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private TaskTypeDto? item;
    private bool isLoading = true;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            item = await Api.GetByIdAsync(Id);   // GET api/tasktypes/{id}
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task ConfirmDelete()
    {
        if (item is null) return;

        var ok = await Api.DeleteAsync(Id);     // DELETE api/tasktypes/{id}
        if (ok)
            Nav.NavigateTo("/tasktypes", forceLoad: true);
        else
            error = "Failed to delete task type.";
    }

    private void Cancel() => Nav.NavigateTo("/tasktypes");
}
