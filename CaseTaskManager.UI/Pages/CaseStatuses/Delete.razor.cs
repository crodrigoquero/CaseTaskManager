using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Pages.CaseStatuses;

public partial class Delete : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseStatusApiService Api { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private CaseStatusDto? item;
    private bool isLoading = true;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            item = await Api.GetByIdAsync(Id); // GET api/casestatuses/{id}
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task ConfirmDelete()
    {
        if (item is null) return;

        var ok = await Api.DeleteAsync(Id); // DELETE api/casestatuses/{id}
        if (ok)
            Nav.NavigateTo("/casestatuses", forceLoad: true);
        else
            error = "Failed to delete case status.";
    }

    private void Cancel() => Nav.NavigateTo("/casestatuses");
}
