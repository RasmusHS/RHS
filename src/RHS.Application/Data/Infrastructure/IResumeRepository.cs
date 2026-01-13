using RHS.Domain.Resume;

namespace RHS.Application.Data.Infrastructure;

public interface IResumeRepository : IAsyncRepository<ResumeEntity>
{
    Task<IReadOnlyList<ResumeEntity>> GetAllAsync();
    Task<ResumeEntity> GetByIdIncludeProjectsAsync(object id);
}