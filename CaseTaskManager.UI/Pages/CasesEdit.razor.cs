using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages;

public partial class CasesEdit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseApiService CaseApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateCaseDetailsDto? model;
    private bool loading = true;
    private bool saving = false;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        var caseItem = await CaseApi.GetCaseByIdAsync(Id);
        if (caseItem != null)
        {
            model = new UpdateCaseDetailsDto
            {
                Title = caseItem.Title,
                Description = caseItem.Description
            };
        }
        loading = false;
    }

    private async Task HandleValidSubmit()
    {
        if (model is null) return;

        saving = true;
        var ok = await CaseApi.UpdateCaseAsync(Id, model);
        saving = false;

        if (ok)
            Nav.NavigateTo("/cases", forceLoad: true);
        else
            error = "Failed to update case.";
    }

    private void Cancel() => Nav.NavigateTo("/cases");
}
