using System.Net.Http.Json;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.TaskType;

namespace CaseTaskManager.UI.Services
{
    public class TaskTypeApiService : ITaskTypeApiService
    {
        private readonly HttpClient _http;

        public TaskTypeApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TaskTypeDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<TaskTypeDto>>("tasktypes/get/all/tasktypes") ?? new();
        }

        public async Task<TaskTypeDto?> GetByIdAsync(int id)
        {
            // handle 404 gracefully instead of throwing
            var resp = await _http.GetAsync($"tasktypes/{id}");
            if (!resp.IsSuccessStatusCode)
            {
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode(); // will throw for other errors
            }
            return await resp.Content.ReadFromJsonAsync<TaskTypeDto>();
        }

        public async Task<bool> UpdateAsync(int id, UpdateTaskTypeDto dto)
        {
            var resp = await _http.PutAsJsonAsync($"tasktypes/{id}", dto); // adjust route if different
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"tasktypes/delete/{id}");
            return response.IsSuccessStatusCode;
        }



        public async Task<bool> CreateAsync(CreateTaskTypeDto dto) =>
            (await _http.PostAsJsonAsync("tasktypes/create/task/type", dto)).IsSuccessStatusCode;

            }
}
