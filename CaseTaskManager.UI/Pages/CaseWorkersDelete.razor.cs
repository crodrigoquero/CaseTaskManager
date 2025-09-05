using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages;

public partial class CaseWorkersDelete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseWorkerApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private CaseWorkerDto? worker;
    private bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        worker = await Api.GetByIdAsync(Id);
        isLoading = false;
    }

    private async Task ConfirmDelete()
    {
        if (worker is null) return;

        var ok = await Api.DeleteAsync(Id);
        if (ok)
            Nav.NavigateTo("/caseworkers", forceLoad: true);
    }

    private void Cancel() => Nav.NavigateTo("/caseworkers");
}
