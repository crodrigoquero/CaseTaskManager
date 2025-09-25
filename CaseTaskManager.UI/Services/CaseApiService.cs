using System.Net.Http.Json;
using CaseTaskManager.UI.Interfaces;
using CaseTaskManager.UI.Models.Case;

namespace CaseTaskManager.UI.Services;


public class CaseApiService : ICaseApiService
{
    private readonly HttpClient _http;

    public CaseApiService(HttpClient http) => _http = http;

    public async Task<List<CaseItem>> GetAllCasesAsync()
        => await _http.GetFromJsonAsync<List<CaseItem>>("cases/get/all/cases") ?? new();

    public async Task<CaseItem?> GetCaseByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"cases/get/case/{id}");
        if (!resp.IsSuccessStatusCode) return null; // tolerate 404
        return await resp.Content.ReadFromJsonAsync<CaseItem>();
    }

    public async Task<bool> CreateCaseAsync(CreateCaseDto newCase)
    {
        var resp = await _http.PostAsJsonAsync("cases/create/case", newCase);
        return resp.IsSuccessStatusCode;
    }

    // Optional: if API returns { id: 123 } on create
    public async Task<int?> CreateCaseAndReturnIdAsync(CreateCaseDto newCase)
    {
        var resp = await _http.PostAsJsonAsync("cases/create/case", newCase);
        if (!resp.IsSuccessStatusCode) return null;

        var payload = await resp.Content.ReadFromJsonAsync<Dictionary<string, int>>();
        return payload is not null && payload.TryGetValue("id", out var id) ? id : null;
    }

    public async Task<bool> UpdateCaseAsync(int id, UpdateCaseDetailsDto dto)
    {
        bool detailsOk = true; // assume OK to avoid blocking when API doesn't support this route
        bool statusOk = true;

        try
        {
            var detailsPayload = new
            {
                title = dto.Title,
                description = dto.Description
            };

            var putResp = await _http.PutAsJsonAsync($"cases/{id}", detailsPayload);
            if (!putResp.IsSuccessStatusCode)
            {
                detailsOk = false;
                var body = await putResp.Content.ReadAsStringAsync();
                Console.WriteLine($"PUT details failed {(int)putResp.StatusCode} {putResp.ReasonPhrase} -> {_http.BaseAddress}cases/{id}\n{body}");
            }
        }
        catch (Exception ex)
        {
            detailsOk = false;
            Console.WriteLine($"PUT details exception: {ex.Message}");
        }

        if (dto.StatusId is not null)
        {
            try
            {
                var patchResp = await _http.PatchAsJsonAsync(
                    $"cases/update/case/{id}/status",
                    new { StatusId = dto.StatusId.Value }
                );

                if (!patchResp.IsSuccessStatusCode)
                {
                    statusOk = false;
                    var body = await patchResp.Content.ReadAsStringAsync();
                    Console.WriteLine($"PATCH status failed {(int)patchResp.StatusCode} {patchResp.ReasonPhrase} -> {_http.BaseAddress}cases/update/case/{id}/status\n{body}");
                }
            }
            catch (Exception ex)
            {
                statusOk = false;
                Console.WriteLine($"PATCH status exception: {ex.Message}");
            }
        }

        // Consider the operation successful if either part succeeded.
        return detailsOk || statusOk;
    }




    public async Task<bool> DeleteCaseAsync(int id)
    {
        var resp = await _http.DeleteAsync($"cases/delete/case/{id}");
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateCaseStatusAsync(int caseId, int statusId)
    {
        var resp = await _http.PatchAsync($"cases/update/case/{caseId}/status",
            JsonContent.Create(new UpdateCaseStatusDto { StatusId = statusId }));
        return resp.IsSuccessStatusCode;
    }
}
