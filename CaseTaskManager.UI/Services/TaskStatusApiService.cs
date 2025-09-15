using System.Net.Http.Json;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;
using CaseTaskManager.UI.Models.TaskStatus;

namespace CaseTaskManager.UI.Services
{
    public class TaskStatusApiService : ITaskStatusApiService
    {
        private readonly HttpClient _http;
        public TaskStatusApiService(HttpClient http) => _http = http;

        public async Task<List<TaskStatusDto>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<TaskStatusDto>>("taskstatuses/get/all") ?? new();

        public async Task<TaskStatusDto?> GetByIdAsync(int id)
        {
            var resp = await _http.GetAsync($"taskstatuses/{id}");
            if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<TaskStatusDto>();
        }

        public async Task<bool> CreateAsync(CreateCaseStatusDto newStatus)
        {
            var resp = await _http.PostAsJsonAsync("taskstatuses/create", newStatus);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, UpdateCaseStatusDto updateDto)
        {
            var resp = await _http.PutAsJsonAsync($"taskstatuses/{id}", updateDto);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"taskstatuses/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}
