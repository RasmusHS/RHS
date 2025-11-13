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

    protected BaseIntegrationTest(RHSWebApplicationFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();

        DbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    }
    
    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
        DbContext.Projects.ExecuteDelete();
        DbContext.Resumes.ExecuteDelete();
    }
}