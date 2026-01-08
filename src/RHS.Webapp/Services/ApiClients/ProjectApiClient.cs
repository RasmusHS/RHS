using RHS.Webapp.Models.Project.Command;
using RHS.Webapp.Models.Project.Query;
using RHS.Webapp.Services.ApiClients.Interfaces;

namespace RHS.Webapp.Services.ApiClients;

public class ProjectApiClient : IProjectApiClient
{
    private readonly HttpClient _httpClient;
    
    public ProjectApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CreateProjectModel> CreateProjectAsync(CreateProjectModel model)
    {
        return await _httpClient.PostAsJsonAsync("api/project/createProject", model).Result.Content.ReadFromJsonAsync<CreateProjectModel>();
    }

    public async Task<QueryProjectModel> GetProjectAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<QueryProjectModel>($"api/project/getProject/{id}");
    }

    public async Task<List<QueryProjectModel>> GetProjectsByResumeIdAsync(Guid resumeId)
    {
        return await _httpClient.GetFromJsonAsync<List<QueryProjectModel>>($"api/project/{resumeId}");
    }

    public async Task<UpdateProjectModel> UpdateProjectAsync(UpdateProjectModel model)
    {
        return await _httpClient.PutAsJsonAsync("api/project/updateProject", model).Result.Content.ReadFromJsonAsync<UpdateProjectModel>();
    }

    public async Task<DeleteProjectModel> DeleteProjectAsync(DeleteProjectModel model)
    {
        return await _httpClient.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(_httpClient.BaseAddress + "api/project/deleteProject"),
            Content = JsonContent.Create(model)
        }).Result.Content.ReadFromJsonAsync<DeleteProjectModel>();
    }
}
