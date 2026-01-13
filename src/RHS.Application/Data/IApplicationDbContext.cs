using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RHS.Domain.Resume;
using RHS.Domain.Resume.Entities;

namespace RHS.Application.Data;

public interface IApplicationDbContext
{
    DatabaseFacade Database { get; }
    
    public DbSet<ResumeEntity> Resumes { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    
    void SaveChanges(CancellationToken cancellationToken = default);
}