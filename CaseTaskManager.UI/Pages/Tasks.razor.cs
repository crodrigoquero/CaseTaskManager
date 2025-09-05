using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;
using CaseTaskManager.UI.Services;
using Microsoft.AspNetCore.Components;

namespace CaseTaskManager.UI.Pages;

public partial class Tasks
{
    [Inject]
    private ITaskApiService TaskApi { get; set; } = default!;

    private List<TaskItem> allTasks = new();
    [Inject] private NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        allTasks = await TaskApi.GetAllTasksAsync();
    }

    private void AddNewTask()
    {
        Navigation.NavigateTo("/tasks/create");
    }

    private void EditTask(int id) => Navigation.NavigateTo($"/tasks/edit/{id}");
    private void GoToDelete(int id) => Navigation.NavigateTo($"/tasks/delete/{id}"); // confirmation page route

}
