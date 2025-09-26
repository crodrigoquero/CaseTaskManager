using Microsoft.AspNetCore.Components;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Tasks;

namespace CaseTaskManager.UI.Pages.Tasks;

public partial class Read : ComponentBase
{
    [Parameter] public int Id { get; set; }

    [Inject] private ITaskApiService TaskApi { get; set; } = default!;
    [Inject] private NavigationManager Nav { get; set; } = default!;

    private TaskItem? item;
    private bool loading = true;

    protected override async Task OnInitializedAsync()
    {
        item = await TaskApi.GetTaskByIdAsync(Id);
        loading = false;
    }

    private void Back() => Nav.NavigateTo("/tasks");
}
