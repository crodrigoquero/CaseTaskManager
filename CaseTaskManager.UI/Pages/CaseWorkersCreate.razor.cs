using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages
{
    public partial class CaseWorkersCreate
    {
        [Inject] private ICaseWorkerApiService Api { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        private CreateCaseWorkerDto model = new();
        private EditContext? editContext;

        private bool saving;
        private string? error;

        protected override void OnInitialized()
        {
            editContext = new EditContext(model);
        }


        private void AddNew() => Nav.NavigateTo("/caseworkers/create");

        private async Task HandleValidSubmit()
        {
            if (saving) return;
            saving = true;
            error = null;

            try
            {
                var ok = await Api.CreateAsync(model);
                if (ok)
                    Nav.NavigateTo("/caseworkers", forceLoad: true);
                else
                    error = "Failed to create case worker.";
            }
            catch (Exception ex)
            {
                error = $"Error: {ex.Message}";
            }
            finally
            {
                saving = false;
            }
        }

        private void Cancel() => Nav.NavigateTo("/caseworkers");
    }
}
