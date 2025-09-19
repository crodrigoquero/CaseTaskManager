using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.CaseWorker;

public class CaseWorkerApiService : ICaseWorkerApiService
{
    private readonly HttpClient _http;

    public CaseWorkerApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CaseWorkerDto>> GetAllAsync() =>
        await _http.GetFromJsonAsync<List<CaseWorkerDto>>("caseworkers/get/all/caseworkers") ?? new();

    public async Task<CaseWorkerDto?> GetByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"caseworkers/{id}");
        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<CaseWorkerDto>();
    }

    public async Task<int> AddAsync(CaseWorkerDto caseWorker)
    {
        var response = await _http.PostAsJsonAsync("caseworkers/add", caseWorker);
        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, int>>();
        return result?["newCaseWorkerId"] ?? 0;
    }

    public async Task<bool> UpdateAsync(int id, CaseWorkerDto caseWorker) =>
        (await _http.PutAsJsonAsync($"caseworkers/{id}", caseWorker)).IsSuccessStatusCode;

    public async Task<bool> ActivateAsync(int id)
    {
        using var req = new HttpRequestMessage(HttpMethod.Patch, $"caseworkers/activate/caseworker/{id}");
        var resp = await _http.SendAsync(req);
        return resp.IsSuccessStatusCode;     // expect 204
    }

    public async Task<bool> DeactivateAsync(int id)
    {
        using var req = new HttpRequestMessage(HttpMethod.Patch, $"caseworkers/deactivate/caseworker/{id}");
        var resp = await _http.SendAsync(req);
        return resp.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteAsync(int id) =>
        (await _http.DeleteAsync($"caseworkers/{id}")).IsSuccessStatusCode;

    public async Task<bool> CreateAsync(CreateCaseWorkerDto dto) =>
    (await _http.PostAsJsonAsync("caseworkers/create/caseworker", dto)).IsSuccessStatusCode;
}
