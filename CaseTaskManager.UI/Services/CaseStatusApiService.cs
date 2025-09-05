using System.Net.Http.Json;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseStatus;

namespace CaseTaskManager.UI.Services
{
    public class CaseStatusApiService : ICaseStatusApiService
    {
        private readonly HttpClient _http;

        public CaseStatusApiService(HttpClient http) => _http = http;

        public async Task<List<CaseStatusDto>> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<CaseStatusDto>>("casestatuses/get/all")
                   ?? new List<CaseStatusDto>();
        }

        public async Task<CaseStatusDto?> GetByIdAsync(int id)
        {
            // API route is GET api/CaseStatuses/{id}
            var resp = await _http.GetAsync($"casestatuses/{id}");
            if (!resp.IsSuccessStatusCode) return null; // be tolerant to 404
            return await resp.Content.ReadFromJsonAsync<CaseStatusDto>();
        }

        public async Task<bool> CreateAsync(CreateCaseStatusDto newStatus)
        {
            var response = await _http.PostAsJsonAsync("casestatuses/create", newStatus);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(int id, UpdateCaseStatusDto updateDto)
        {
            // API route is PUT api/CaseStatuses/{id}
            var response = await _http.PutAsJsonAsync($"casestatuses/{id}", updateDto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // API route is DELETE api/CaseStatuses/{id}
            var response = await _http.DeleteAsync($"casestatuses/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
