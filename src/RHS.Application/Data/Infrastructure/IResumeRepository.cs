using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.Data.Infrastructure;

public interface IResumeRepository : IAsyncRepository<ResumeEntity>
{
    Task<IEnumerable<ProjectEntity>> AddRangeProjectsAsync(List<ProjectEntity> entities, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ResumeEntity>> GetAllAsync();
}