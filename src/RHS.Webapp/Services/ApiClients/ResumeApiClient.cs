using RHS.Webapp.Models.Resume.Command;
using RHS.Webapp.Models.Resume.Query;
using RHS.Webapp.Services.ApiClients.Interfaces;

namespace RHS.Webapp.Services.ApiClients;

public class ResumeApiClient : IResumeApiClient
{
    private readonly HttpClient _httpClient;

    public ResumeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CreateResumeModel> CreateResumeAsync(CreateResumeModel model)
    {
        return await _httpClient.PostAsJsonAsync("/api/resume", model).Result.Content.ReadFromJsonAsync<CreateResumeModel>();
    }

    public async Task<QueryResumeModel> GetResumeAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<QueryResumeModel>($"/api/resume/{id}");
    }

    public async Task<UpdateResumeModel> UpdateResumeAsync(UpdateResumeModel model)
    {
        return await _httpClient.PutAsJsonAsync("/api/resume", model).Result.Content.ReadFromJsonAsync<UpdateResumeModel>();
    }
}
