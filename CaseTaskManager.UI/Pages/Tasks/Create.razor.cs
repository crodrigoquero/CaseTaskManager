using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Models.Tasks;
using CaseTaskManager.UI.Interfaces;


namespace CaseTaskManager.UI.Pages.Tasks
{
    public partial class Create
    {
        [Inject] private ITaskApiService TaskApi { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        private CreateTaskDto newTask = new()
        {
            DueDate = DateTime.Today
        };

        private string? error;

        private async Task HandleValidSubmit()
        {
            var ok = await TaskApi.CreateTaskAsync(newTask);
            if (ok)
                Navigation.NavigateTo("/tasks", forceLoad: true);
            else
                error = "Failed to create task.";
        }

        private void Cancel() => Navigation.NavigateTo("/tasks");
    }
}

