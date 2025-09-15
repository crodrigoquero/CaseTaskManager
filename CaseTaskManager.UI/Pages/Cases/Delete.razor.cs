using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages.Cases;

public partial class Delete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseApiService CaseApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private CaseItem? item;
    private bool loading = true;

    protected override async Task OnInitializedAsync()
    {
        item = await CaseApi.GetCaseByIdAsync(Id);
        loading = false;
    }

    private async Task DeleteCase()
    {
        var ok = await CaseApi.DeleteCaseAsync(Id);
        if (ok) Nav.NavigateTo("/cases", forceLoad: true);
        // else: optionally show an error
    }

    private void Cancel() => Nav.NavigateTo("/cases");
}
