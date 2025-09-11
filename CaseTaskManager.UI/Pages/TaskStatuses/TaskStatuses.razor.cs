using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages.TaskStatuses
{
    public partial class TaskStatuses : ComponentBase
    {
        [Inject] private ITaskStatusApiService TaskStatusService { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        protected List<TaskStatusDto> allStatuses = new();
        protected string newStatusName = string.Empty;
        protected string? error;

        protected override async Task OnInitializedAsync()
        {
            allStatuses = await TaskStatusService.GetAllAsync();
        }

        protected async Task AddTaskStatus()
        {
            error = null;

            if (string.IsNullOrWhiteSpace(newStatusName))
                return;

            var ok = await TaskStatusService.CreateAsync(new CreateCaseStatusDto
            {
                StatusName = newStatusName,
                Description = null
            });

            if (ok)
            {
                allStatuses = await TaskStatusService.GetAllAsync();
                newStatusName = string.Empty;
                StateHasChanged();
            }
            else
            {
                error = "Failed to create task status.";
            }
        }

        // NEW: navigate to edit page
        protected void EditTaskStatus(int id) => Nav.NavigateTo($"/taskstatuses/edit/{id}");

        // FIXED: correct delete page route (lowercase path)
        protected void GoToDelete(int id) => Nav.NavigateTo($"/Task/Statuses/delete/{id}");
    }
}
