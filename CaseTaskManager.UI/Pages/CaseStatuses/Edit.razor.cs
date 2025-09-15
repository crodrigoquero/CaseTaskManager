using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages.CaseStatuses;

public partial class Edit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateCaseStatusDto model = new();
    private bool loading = true;
    private bool saving;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        var dto = await Api.GetByIdAsync(Id);
        if (dto is null)
        {
            error = "Case status not found.";
            loading = false;
            return;
        }

        model = new UpdateCaseStatusDto
        {
            StatusName = dto.StatusName,
            Description = dto.Description
        };

        loading = false;
    }

    private async Task HandleValidSubmit()
    {
        if (saving) return;
        saving = true;
        error = null;

        var ok = await Api.UpdateAsync(Id, model);
        if (ok) Nav.NavigateTo("/casestatuses", forceLoad: true);
        else error = "Failed to save changes.";

        saving = false;
    }

    private void Cancel() => Nav.NavigateTo("/casestatuses");
}
