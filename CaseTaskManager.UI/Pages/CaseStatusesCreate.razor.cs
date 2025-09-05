using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Pages;

public partial class CaseStatusesCreate : ComponentBase
{
    [Inject] private ICaseStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private CreateCaseStatusDto model = new();
    private bool saving;
    private string? error;

    private async Task HandleValidSubmit()
    {
        if (saving) return;
        saving = true;
        error = null;

        try
        {
            var ok = await Api.CreateAsync(model);
            if (ok)
                Nav.NavigateTo("/casestatuses", forceLoad: true);
            else
                error = "Failed to create case status.";
        }
        catch (Exception ex)
        {
            error = $"Error: {ex.Message}";
        }
        finally
        {
            saving = false;
        }
    }

    private void Cancel() => Nav.NavigateTo("/casestatuses");
}
