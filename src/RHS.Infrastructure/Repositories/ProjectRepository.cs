using System.Data;
using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume.Entities;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly IApplicationDbContext _dbContext;
    
    public ProjectRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ProjectEntity> AddAsync(ProjectEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        
        await _dbContext.Projects.AddAsync(entity, cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        Save(cancellationToken);
        
        return await Task.FromResult((ProjectEntity)entity);
    }

    public async Task<IEnumerable<ProjectEntity>> AddRangeAsync(List<ProjectEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        
        await _dbContext.Projects.AddRangeAsync(entities, cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        Save(cancellationToken);
        
        return await Task.FromResult((IEnumerable<ProjectEntity>)entities);
    }

    public async Task<ProjectEntity> GetByIdAsync(object id)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        var result = await _dbContext.Projects.FindAsync(id) ?? throw new KeyNotFoundException($"Project with ID {id} not found.");
        await _dbContext.Database.CommitTransactionAsync();
        
        return result;
    }
    
    public async Task<IReadOnlyList<ProjectEntity>> GetAllByResumeIdAsync(object resumeId)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        var result = await _dbContext.Projects.AsNoTracking()
            .Where(p => p.ResumeId == (ResumeId)resumeId)
            .ToListAsync() ?? throw new KeyNotFoundException($"Projects for Resume ID {resumeId} not found.");
        await _dbContext.Database.CommitTransactionAsync();
        if (result.Count() < 1)
        {
            throw new KeyNotFoundException($"Projects for Resume ID {resumeId} not found.");
        }
        
        return result;
    }

    public async Task UpdateAsync(ProjectEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Projects.Attach(entity);
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
        _dbContext.Projects.Update(entity);
        Save(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var entity = await _dbContext.Projects.FindAsync(id) ?? throw new KeyNotFoundException($"Project with ID {id} not found.");
        _dbContext.Projects.Remove(entity);
        Save(cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
    
    public void Save(CancellationToken cancellationToken = default)
    {
        _dbContext.SaveChanges(cancellationToken); ;
    }
}