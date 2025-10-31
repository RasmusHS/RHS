using System.Data;
using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Resume.Entities;

namespace RHS.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IApplicationDbContext _dbContext;
    
    public ProjectRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<ProjectEntity> AddAsync(ProjectEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        
        _dbContext.Projects.AddAsync(entity, cancellationToken);
        _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return Task.FromResult((ProjectEntity)entity);
    }

    public Task<IEnumerable<ProjectEntity>> AddRangeAsync(List<ProjectEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        
        _dbContext.Projects.AddRangeAsync(entities, cancellationToken);
        _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return Task.FromResult((IEnumerable<ProjectEntity>)entities);
    }

    public Task<Result<ProjectEntity>> GetByIdAsync(object id)
    {
        throw new NotImplementedException();
    }
    
    public Task<IReadOnlyList<ProjectEntity>> GetAllByResumeIdAsync(object resumeId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ProjectEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public void Save(CancellationToken cancellationToken = default)
    {
        _dbContext.SaveChanges(cancellationToken); ;
    }
}