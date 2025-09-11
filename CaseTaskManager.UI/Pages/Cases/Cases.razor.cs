using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages.Cases;

public partial class Cases : ComponentBase
{
    [Inject] public ICaseApiService CaseApiService { get; set; } = default!;
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected List<CaseItem>? allCases;

    protected override async Task OnInitializedAsync()
    {
        allCases = await CaseApiService.GetAllCasesAsync();
    }

    private void EditCase(int id) => Nav.NavigateTo($"/cases/edit/{id}");

    private async Task DeleteCase(int id)
    {
        var ok = await CaseApiService.DeleteCaseAsync(id);
        if (ok)
        {
            allCases?.RemoveAll(c => c.Id == id);
            StateHasChanged();
        }
        // Optionally show a toast/error if not ok
    }

    private void AddNewCase() => Nav.NavigateTo("/cases/create");

    private void GoToDelete(int id) => Nav.NavigateTo($"/cases/delete/{id}");

}
