using RHS.Domain.Resume.Entities;

namespace RHS.Application.Data.Infrastructure;

public interface IProjectRepository : IAsyncRepository<ProjectEntity>
{
    Task<IReadOnlyList<ProjectEntity>> GetAllByResumeIdAsync(object resumeId);
}