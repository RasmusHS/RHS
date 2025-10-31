//using System.Transactions;

using System.Data;
using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Common;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;

namespace RHS.Infrastructure.Repositories;

public class ResumeRepository : IResumeRepository
{
    private readonly IApplicationDbContext _dbContext;
    
    public ResumeRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ResumeEntity> AddAsync(ResumeEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        // if (entity.Projects.Any())
        // {
        //     _dbContext.Resumes.AddAsync(entity, cancellationToken);
        //     _dbContext.Projects.AddRangeAsync(entity.Projects, cancellationToken);
        //     
        //     _dbContext.Database.CommitTransactionAsync(cancellationToken);
        //     
        //     return Task.FromResult((ResumeEntity)entity);
        // }
        
        _dbContext.Resumes.AddAsync(entity, cancellationToken);
        
        _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return Task.FromResult((ResumeEntity)entity);
    }

    public Task<IEnumerable<ResumeEntity>> AddRangeAsync(List<ResumeEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public Task<IEnumerable<ProjectEntity>> AddRangeProjectsAsync(List<ProjectEntity> entities, CancellationToken cancellationToken = default)
    {
        _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        
        _dbContext.Projects.AddRangeAsync(entities, cancellationToken);
        _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return Task.FromResult((IEnumerable<ProjectEntity>)entities);
    }
    
    public Task<Result<ResumeEntity>> GetByIdAsync(object id)
    {
        throw new NotImplementedException();
    }
    
    public Task<IReadOnlyList<ResumeEntity>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(ResumeEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Save(CancellationToken cancellationToken = default)
    {
        _dbContext.SaveChanges(cancellationToken);
    }
}