using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseWorker;
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

    protected async Task ToggleStatus(int id)
    {
        var item = allCaseWorkers.FirstOrDefault(w => w.Id == id);
        if (item is null) return;

        var original = item.IsActive;
        item.IsActive = !original;   // optimistic UI update
        StateHasChanged();

        try
        {
            var ok = original
                ? await CaseWorkerService.DeactivateAsync(id)
                : await CaseWorkerService.ActivateAsync(id);

            if (!ok)
            {
                item.IsActive = original; // revert on failure
                StateHasChanged();
            }
        }
        catch
        {
            item.IsActive = original;     // revert on exception
            StateHasChanged();
        }
    }

    private void NavigateToEdit(int id) => Nav.NavigateTo($"/caseworkers/edit/{id}");
    private void NavigateToDelete(int id) => Nav.NavigateTo($"/caseworkers/delete/{id}");
    private void AddNew() => Nav.NavigateTo("/caseworkers/create");
}
