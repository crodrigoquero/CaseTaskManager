using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Models.Tasks;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.TaskStatus;
using CaseTaskManager.UI.Models.TaskType;
using CaseTaskManager.UI.Pages.Cases;
using CaseTaskManager.UI.Models.Case;


namespace CaseTaskManager.UI.Pages.Tasks
{
    public partial class Create
    {
        [Inject] private ITaskApiService TaskApi { get; set; } = default!;
        [Inject] private ITaskStatusApiService TaskStatusApi { get; set; } = default!;
        [Inject] private ITaskTypeApiService TaskTypeApi { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        [Inject] private ICaseApiService CaseApi { get; set; } = default!;   // NEW

        private List<CaseItem> cases = new(); // NEW

        private CreateTaskDto newTask = new() { DueDate = DateTime.Today };

        private List<TaskStatusDto> taskStatuses = new();
        private List<TaskTypeDto> taskTypes = new();
        private string? error;
        private bool submitting;

        protected override async Task OnInitializedAsync()
        {
            // Load dropdown data in parallel
            var statusesTask = TaskStatusApi.GetAllAsync();   // method name: use your actual one
            var typesTask = TaskTypeApi.GetAllAsync();     // method name: use your actual one

            var casesTask = CaseApi.GetAllCasesAsync();     // NEW (use your actual method name)

            await Task.WhenAll(statusesTask, typesTask, casesTask);

            taskStatuses = statusesTask.Result?.ToList() ?? new();
            taskTypes = typesTask.Result?.ToList() ?? new();
            cases = casesTask.Result?.ToList() ?? new();


            await Task.WhenAll(statusesTask, typesTask);

            taskStatuses = statusesTask.Result?.ToList() ?? new();
            taskTypes = typesTask.Result?.ToList() ?? new();

            // Optional: preselect first items if you want defaults
            // if (taskStatuses.Count > 0) newTask.StatusId = taskStatuses[0].Id;
            // if (taskTypes.Count > 0)    newTask.TaskTypeId = taskTypes[0].Id;
        }

        private async Task HandleValidSubmit()
        {
            error = null;
            submitting = true;
            try
            {
                var ok = await TaskApi.CreateTaskAsync(newTask);
                if (ok)
                {
                    Navigation.NavigateTo("/tasks", forceLoad: true);
                    return;
                }
                error = "Failed to create task.";
            }
            catch (Exception ex)
            {
                error = $"Failed to create task: {ex.Message}";
            }
            finally
            {
                submitting = false;
            }
        }

        private void Cancel() => Navigation.NavigateTo("/tasks");
    }
}
