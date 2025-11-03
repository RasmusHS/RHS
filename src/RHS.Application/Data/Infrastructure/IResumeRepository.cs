using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.Data.Infrastructure;

public interface IResumeRepository : IAsyncRepository<ResumeEntity>
{
    Task<IReadOnlyList<ResumeEntity>> GetAllAsync();
}