using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages;

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
        if (isActive)
            await CaseWorkerService.DeactivateAsync(id);
        else
            await CaseWorkerService.ActivateAsync(id);

        await RefreshData();
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
