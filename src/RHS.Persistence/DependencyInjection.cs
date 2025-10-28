using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RHS.Application.Data;

namespace RHS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .UseMySql(serverVersion: ServerVersion.AutoDetect(configuration.GetConnectionString("Live")), connectionString: configuration.GetConnectionString("Live"))
                .UseSnakeCaseNamingConvention());
        
        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}