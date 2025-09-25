using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;
using CaseTaskManager.UI.Models.CaseStatus; // adjust if your DTO namespace differs
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages.Cases;

public partial class Cases : ComponentBase
{
    [Inject] public ICaseApiService CaseApiService { get; set; } = default!;
    [Inject] public ICaseStatusApiService CaseStatusApi { get; set; } = default!; // NEW
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected List<CaseItem>? allCases;
    private Dictionary<int, string>? statusLookup;
    private bool isLoading = true;                 // optional UX flag

    private void ViewDetails(int id) => Nav.NavigateTo($"/cases/details/{id}");

    protected override async Task OnInitializedAsync()
    {
        isLoading = true;

        // load cases + statuses in parallel for speed
        var casesTask = CaseApiService.GetAllCasesAsync();       // existing method
        var statusesTask = CaseStatusApi.GetAllAsync();         // expected method on CaseStatus API

        await Task.WhenAll(casesTask, statusesTask);

        allCases = (await casesTask) as List<CaseItem> ?? (await casesTask).ToList();

        var statuses = (await statusesTask) as IEnumerable<CaseStatusDto> ?? (await statusesTask).ToList();
        statusLookup = statuses.ToDictionary(s => s.Id, s => s.StatusName ?? string.Empty);

        isLoading = false;
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
