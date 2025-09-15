using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages.TaskTypes
{
    public partial class Create : ComponentBase
    {
        [Inject] private ITaskTypeApiService Api { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        private CreateTaskTypeDto model = new();
        private bool saving;
        private string? error;

        private async Task HandleValidSubmit()
        {
            if (saving) return;
            saving = true;
            error = null;

            try
            {
                var ok = await Api.CreateAsync(model);
                if (ok)
                    Nav.NavigateTo("/tasktypes", forceLoad: true);
                else
                    error = "Failed to create task type.";
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

        private void Cancel() => Nav.NavigateTo("/tasktypes");
    }
}
