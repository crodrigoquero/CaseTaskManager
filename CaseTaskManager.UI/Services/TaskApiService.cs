using CaseTaskManager.UI.Models;
using CaseTaskManager.UI.Models.Tasks;
using CaseTaskManager.UI.Interfaces;

namespace CaseTaskManager.UI.Services
{
    public class TaskApiService : ITaskApiService
    {
        private readonly HttpClient _http;

        public TaskApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var tasks = await _http.GetFromJsonAsync<List<TaskItem>>("tasks/get/all/tasks");
            return tasks ?? new List<TaskItem>();
        }

        public async Task<bool> CreateTaskAsync(CreateTaskDto newTask)
        {
            var response = await _http.PostAsJsonAsync("tasks/create", newTask);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var resp = await _http.DeleteAsync($"tasks/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskStatusAsync(int id, int statusId)
        {
            var payload = new { StatusId = statusId };
            var resp = await _http.PatchAsJsonAsync($"tasks/{id}/status", payload);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var response = await _http.PutAsJsonAsync($"tasks/{id}", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            var resp = await _http.GetAsync($"tasks/get/task/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<TaskItem>();
        }

    }
}
