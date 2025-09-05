using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models;

namespace CaseTaskManager.UI.Pages
{
    public partial class TaskTypes
    {
        [Inject] private ITaskTypeApiService TaskTypeService { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        protected List<TaskTypeDto> allTaskTypes = new();

        protected override async Task OnInitializedAsync()
        {
            allTaskTypes = await TaskTypeService.GetAllAsync();
        }


        protected void GoToDelete(int id) => Nav.NavigateTo($"/tasktypes/delete/{id}");

        private void AddNew() => Nav.NavigateTo("/tasktypes/create");

        private void EditTaskType(int id) => Nav.NavigateTo($"/tasktypes/edit/{id}");


    }
}
