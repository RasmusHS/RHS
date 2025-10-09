using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RHS.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<>(options =>
            options.UseMySql(configuration.GetConnectionString("Live"))
                .UseSnakeCaseNamingConvention());
        
        return services;
    }
}