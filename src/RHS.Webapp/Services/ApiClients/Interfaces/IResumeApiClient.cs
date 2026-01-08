using RHS.Webapp.Models.Resume.Command;
using RHS.Webapp.Models.Resume.Query;

namespace RHS.Webapp.Services.ApiClients.Interfaces;

public interface IResumeApiClient
{
    Task<CreateResumeModel> CreateResumeAsync(CreateResumeModel model);
    Task<QueryResumeModel> GetResumeAsync(Guid id);
    Task<UpdateResumeModel> UpdateResumeAsync(UpdateResumeModel model);
}
