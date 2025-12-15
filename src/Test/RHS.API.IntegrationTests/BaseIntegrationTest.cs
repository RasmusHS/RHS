using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RHS.Persistence;

namespace RHS.API.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<RHSWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    //private static bool _databaseInitialized;
    //private static readonly object _lock = new object();

    protected BaseIntegrationTest(RHSWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        DbContext.Database.EnsureCreated();
        // Only create database once for all tests
        //lock (_lock)
        //{
        //    if (!_databaseInitialized)
        //    {
        //        DbContext.Database.EnsureCreated();
        //        _databaseInitialized = true;
        //    }
        //}
        
        // Clean data between tests instead of recreating database
        //CleanDatabase();
    }
    
    private void CleanDatabase()
    {
        // Remove all data but keep schema
        DbContext.Projects.RemoveRange(DbContext.Projects);
        DbContext.Resumes.RemoveRange(DbContext.Resumes);
        // Add other entities...
        DbContext.SaveChanges();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}