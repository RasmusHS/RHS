using System.Collections.Immutable;
using System.Data;
using Microsoft.EntityFrameworkCore;
using RHS.Application.Data;
using RHS.Application.Data.Infrastructure;
using RHS.Domain.Resume;
using RHS.Domain.Resume.ValueObjects;

namespace RHS.Infrastructure.Repositories;

public class ResumeRepository : IResumeRepository
{
    private readonly IApplicationDbContext _dbContext;
    
    public ResumeRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResumeEntity> AddAsync(ResumeEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);

        if (entity.Projects.Any())
        {
            await _dbContext.Resumes.AddAsync(entity, cancellationToken);
            await _dbContext.Projects.AddRangeAsync(entity.Projects, cancellationToken);
            
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
            
            return await Task.FromResult((ResumeEntity)entity);
        }
        
        await _dbContext.Resumes.AddAsync(entity, cancellationToken);
        
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return await Task.FromResult((ResumeEntity)entity);
    }

    public async Task<IEnumerable<ResumeEntity>> AddRangeAsync(List<ResumeEntity> entities, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        await _dbContext.Resumes.AddRangeAsync(entities, cancellationToken);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        
        return await Task.FromResult((IEnumerable<ResumeEntity>)entities);
    }
    
    public async Task<ResumeEntity> GetByIdAsync(object id)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        var result = await _dbContext.Resumes.FindAsync(id) ?? throw new KeyNotFoundException($"Resume with ID {id} not found.");
        await _dbContext.Database.CommitTransactionAsync();
        
        return result;
    }
    
    public async Task<ResumeEntity> GetByIdIncludeProjectsAsync(object id)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        var result = _dbContext.Resumes.AsNoTracking().Include(p => p.Projects).Where(p => p.Id == (ResumeId)id);
        var resume = await result.FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Resume with ID {id} not found.");
        await _dbContext.Database.CommitTransactionAsync();
        return resume;
    }
    
    public async Task<IReadOnlyList<ResumeEntity>> GetAllAsync()
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead);
        var result = await _dbContext.Resumes.AsNoTracking().ToListAsync();
        await _dbContext.Database.CommitTransactionAsync();
        
        return result.ToImmutableList();
    }

    public async Task UpdateAsync(ResumeEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Resumes.Attach(entity);
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted, cancellationToken);
        _dbContext.Resumes.Update(entity);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public async Task DeleteAsync(object id, CancellationToken cancellationToken = default)
    {
        await _dbContext.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead, cancellationToken);
        var entity = await _dbContext.Resumes.FindAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Resume with ID {id} not found.");
        _dbContext.Resumes.Remove(entity);
        await _dbContext.Database.CommitTransactionAsync(cancellationToken);
    }

    public void Save(CancellationToken cancellationToken = default)
    {
        _dbContext.SaveChanges(cancellationToken);
    }
}