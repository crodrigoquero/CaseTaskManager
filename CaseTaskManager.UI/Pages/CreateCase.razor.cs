using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Pages
{
    public partial class CreateCase
    {
        [Inject] private ICaseApiService CaseApi { get; set; } = default!;
        [Inject] private ICaseStatusApiService CaseStatusApi { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private CreateCaseDto newCase = new();
        private EditContext? editContext;

        private List<CaseStatusDto> caseStatuses = new();
        private string? error;
        private bool saving;

        protected override async Task OnInitializedAsync()
        {
            editContext = new EditContext(newCase);

            // Load case statuses for the dropdown
            try
            {
                caseStatuses = await CaseStatusApi.GetAllAsync();
            }
            catch
            {
                // Non-fatal: leave the list empty; user can still create without status
                caseStatuses = new();
            }
        }

        private async Task HandleValidSubmit()
        {
            if (saving) return;
            saving = true;
            error = null;

            try
            {
                var ok = await CaseApi.CreateCaseAsync(newCase);
                if (ok)
                {
                    Navigation.NavigateTo("/cases", forceLoad: true);
                }
                else
                {
                    error = "Failed to create case.";
                }
            }
            catch (Exception ex)
            {
                error = $"Failed to create case: {ex.Message}";
            }
            finally
            {
                saving = false;
            }
        }

        private void Cancel() => Navigation.NavigateTo("/cases");
    }
}
