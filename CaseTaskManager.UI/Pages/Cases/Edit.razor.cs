using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;
using CaseTaskManager.UI.Models.CaseStatus; // adjust namespace if different

namespace CaseTaskManager.UI.Pages.Cases;

public partial class Edit : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ICaseApiService CaseApi { get; set; } = default!;
    [Inject] private ICaseStatusApiService CaseStatusApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private UpdateCaseDetailsDto? model;
    private List<CaseStatusDto>? caseStatuses;
    private bool loading = true;
    private bool saving = false;
    private string? error;

    protected override async Task OnInitializedAsync()
    {
        loading = true;
        try
        {
            var caseTask = CaseApi.GetCaseByIdAsync(Id);
            var statusesTask = CaseStatusApi.GetAllAsync();

            await Task.WhenAll(caseTask, statusesTask);

            var caseItem = caseTask.Result;
            caseStatuses = statusesTask.Result?.ToList() ?? new List<CaseStatusDto>();

            if (caseItem != null)
            {
                model = new UpdateCaseDetailsDto
                {
                    Title = caseItem.Title,
                    Description = caseItem.Description,
                    StatusId = caseItem.CurrentStatusId
                };
            }
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

    private async Task HandleValidSubmit()
    {
        if (model is null) return;

        saving = true;
        error = null;
        try
        {
            var ok = await CaseApi.UpdateCaseAsync(Id, model);
            if (ok)
            {
                Nav.NavigateTo("/cases", forceLoad: true);
                return;
            }

            error = "Failed to update case.";
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            saving = false;
        }
    }

    private void Cancel() => Nav.NavigateTo("/cases");
}
