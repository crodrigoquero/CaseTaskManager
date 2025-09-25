using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;

namespace CaseTaskManager.UI.Pages.Cases;

public partial class Read : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseApiService CaseApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private CaseItem? item;
    private bool loading = true;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            item = await CaseApi.GetCaseByIdAsync(Id);
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            loading = false;
        }
    }

    private void Cancel() => Nav.NavigateTo("/cases");
}
