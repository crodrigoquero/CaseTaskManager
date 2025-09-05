using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages;

public partial class CaseStatuses : ComponentBase
{
    [Inject] private ICaseStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    protected List<CaseStatusDto> allCaseStatuses = new();

    protected override async Task OnInitializedAsync()
    {
        allCaseStatuses = await Api.GetAllAsync();
    }

    protected void AddNew()
    {
        Nav.NavigateTo("/casestatuses/create");
    }

    protected void EditCaseStatus(int id)
    {
        Nav.NavigateTo($"/casestatuses/edit/{id}");
    }

    private void DeleteCaseStatus(int id) => Nav.NavigateTo($"/casestatuses/delete/{id}");
}
