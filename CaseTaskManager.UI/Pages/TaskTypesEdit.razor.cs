using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages;

public partial class TaskTypesEdit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskTypeApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateTaskTypeDto? model;
    private bool loading = true;
    private bool saving;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        var existing = await Api.GetByIdAsync(Id);
        if (existing is not null)
        {
            model = new UpdateTaskTypeDto
            {
                TypeName = existing.TypeName,
                Description = existing.Description
            };
        }
        loading = false;
    }

    private async Task HandleValidSubmit()
    {
        if (model is null) return;

        saving = true;
        error = null;
        try
        {
            var ok = await Api.UpdateAsync(Id, model);
            if (ok)
                Nav.NavigateTo("/tasktypes", forceLoad: true);
            else
                error = "Failed to update task type.";
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            saving = false;
        }
    }

    private void Cancel() => Nav.NavigateTo("/tasktypes");
}
