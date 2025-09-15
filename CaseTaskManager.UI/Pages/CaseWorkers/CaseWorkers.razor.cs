using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages.CaseWorkers;

public partial class CaseWorkers : ComponentBase
{
    [Inject] private ICaseWorkerApiService CaseWorkerService { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!; 

    protected List<CaseWorkerDto> allCaseWorkers = new();
    protected bool isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;
        allCaseWorkers = await CaseWorkerService.GetAllAsync();
        isLoading = false;
    }

    protected async Task ToggleStatus(int id, bool isActive)
    {
        // find the local item once
        var item = allCaseWorkers.FirstOrDefault(w => w.Id == id);
        if (item is null) return;

        // optimistic UI update
        var original = item.IsActive;
        item.IsActive = !original;
        StateHasChanged();                       // re-render immediately

        try
        {
            bool ok;
            if (original) // was active => deactivate
                ok = await CaseWorkerService.DeactivateAsync(id);
            else          // was inactive => activate
                ok = await CaseWorkerService.ActivateAsync(id);

            if (!ok)
            {
                // revert on failure
                item.IsActive = original;
                StateHasChanged();
                // optional: show a toast/message
            }
        }
        catch
        {
            // revert on exception
            item.IsActive = original;
            StateHasChanged();
            // optional: log or show error
        }
    }



    private void NavigateToDelete(int id)
    {
        Nav.NavigateTo($"/caseworkers/delete/{id}");
    }

    private async Task RefreshData()
    {
        allCaseWorkers = await CaseWorkerService.GetAllAsync();
        StateHasChanged();
    }

    private void AddNew() => Nav.NavigateTo("/caseworkers/create"); 
}
