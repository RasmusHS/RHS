using Microsoft.Extensions.DependencyInjection;
using RHS.Application.Data.Infrastructure;
using RHS.Infrastructure.Repositories;

namespace RHS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //services.AddScoped<,>();
        services.AddScoped<IResumeRepository, ResumeRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        return services;
    }
}