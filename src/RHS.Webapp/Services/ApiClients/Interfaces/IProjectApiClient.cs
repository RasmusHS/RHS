using RHS.Webapp.Models.Project.Command;
using RHS.Webapp.Models.Project.Query;

namespace RHS.Webapp.Services.ApiClients.Interfaces;

public interface IProjectApiClient
{
    //Task<CreateProjectModel> CreateProjectAsync(CreateProjectModel model);
    Task<QueryProjectModel> GetProjectAsync(Guid id);
    Task<List<QueryProjectModel>> GetProjectsByResumeIdAsync(Guid resumeId);
    //Task<UpdateProjectModel> UpdateProjectAsync(UpdateProjectModel model);
    //Task<DeleteProjectModel> DeleteProjectAsync(DeleteProjectModel model);
}
