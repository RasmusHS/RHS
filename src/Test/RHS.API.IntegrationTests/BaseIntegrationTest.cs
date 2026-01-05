using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RHS.Persistence;

namespace RHS.API.IntegrationTests;

public abstract class BaseIntegrationTest : IClassFixture<RHSWebApplicationFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    private readonly RHSWebApplicationFactory _factory;
    protected readonly ISender Sender;
    protected readonly ApplicationDbContext DbContext;
    //private static bool _databaseInitialized;
    //private static readonly object _lock = new object();

    protected BaseIntegrationTest(RHSWebApplicationFactory factory)
    {
        _factory = factory;
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        InitializeDatabaseAsync().GetAwaiter().GetResult();
        //DbContext.Database.EnsureCreated();
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

    private async Task InitializeDatabaseAsync()
    {
        // Factory ensures database is created once per container
        await _factory.EnsureDatabaseCreatedAsync(DbContext);

        // Clean data for this test
        await CleanDatabaseAsync();
    }

    private async Task CleanDatabaseAsync()
    {
        // Remove all data from tables
        DbContext.Projects.RemoveRange(DbContext.Projects);
        DbContext.Resumes.RemoveRange(DbContext.Resumes);

        await DbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        if (DbContext != null)
            await DbContext.DisposeAsync();

        _scope?.Dispose();
    }
}