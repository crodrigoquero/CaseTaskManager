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

        public async Task<bool> CreateTaskAsync(CreateTaskDto dto)
        {
            var resp = await _http.PostAsJsonAsync("tasks/create/task", dto); // <-- FIXED
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var relative = $"tasks/delete/task/{id}";        // matches controller: [HttpDelete("delete/task/{id}")]
            var req = new HttpRequestMessage(HttpMethod.Delete, relative);

            var resp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);

            // Success if API returns 200 OK (Option B) or 204 No Content (Option A)
            if (resp.StatusCode == System.Net.HttpStatusCode.OK ||
                resp.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }

            // Dump details to console to spot route/status issues fast
            var absolute = new Uri(_http.BaseAddress!, relative);
            var body = await resp.Content.ReadAsStringAsync();
            Console.WriteLine($"DELETE failed: {(int)resp.StatusCode} {resp.ReasonPhrase} for {absolute}\n{body}");

            return false;
        }


        public async Task<bool> UpdateTaskStatusAsync(int id, int statusId)
        {
            var payload = new { StatusId = statusId };
            var resp = await _http.PatchAsJsonAsync($"tasks/{id}/status", payload);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateTaskAsync(int id, UpdateTaskDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"tasks/update/task/{id}/details", dto);
            return resp.IsSuccessStatusCode;
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
