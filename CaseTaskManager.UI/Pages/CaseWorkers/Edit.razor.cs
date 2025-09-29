using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Models.CaseWorker;

namespace CaseTaskManager.UI.Pages.CaseWorkers
{
    public partial class Edit : ComponentBase
    {
        [Parameter] public int Id { get; set; }

        [Inject] private ICaseWorkerApiService Api { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        private UpdateCaseWorkerDto? model;
        private bool loading = true;
        private bool saving = false;
        private string? error;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var worker = await Api.GetByIdAsync(Id);
                if (worker is not null)
                {
                    model = new UpdateCaseWorkerDto
                    {
                        FullName = worker.FullName,
                        Email = worker.Email,
                        IsActive = worker.IsActive
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
                var ok = await Api.UpdateAsync(Id, model);
                if (ok)
                {
                    Nav.NavigateTo("/caseworkers", forceLoad: true);
                    return;
                }
                error = "Failed to update case worker.";
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

        private void Cancel() => Nav.NavigateTo("/caseworkers");
    }
}
