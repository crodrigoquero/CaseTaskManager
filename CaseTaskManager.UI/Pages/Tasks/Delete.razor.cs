using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages.Tasks;

public partial class Delete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private TaskItem? item;
    private bool isLoading = true;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            item = await Api.GetTaskByIdAsync(Id); // calls GET api/tasks/get/task/{id}
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

        try
        {
            var ok = await Api.DeleteTaskAsync(Id); // calls DELETE api/tasks/delete/task/{id}
            if (ok)
            {
                Nav.NavigateTo("/tasks", forceLoad: true);
            }
            else
            {
                error = "Failed to delete task.";
            }
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
    }

    private void Cancel() => Nav.NavigateTo("/tasks");
}
