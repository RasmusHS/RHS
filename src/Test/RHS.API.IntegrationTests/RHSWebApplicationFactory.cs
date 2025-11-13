using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RHS.Persistence;
using Testcontainers.PostgreSql;

namespace RHS.API.IntegrationTests;

public class RHSWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithName("rhs_dev.db")
        .WithDatabase("rhs_dev")
        .WithUsername("rhs_dev")
        .WithPassword("postgres")
        .WithPortBinding(5432, 5432)
        .WithVolumeMount("./.containers/db", "/var/lib/postgresql/data")
        .WithCleanUp(true)
        .Build();

    //public DbConnection DbConnection => new NpgsqlConnection(((PostgreSqlContainer)_dbContainer).GetConnectionString());
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                    .UseNpgsql(_dbContainer.GetConnectionString())
                    .UseSnakeCaseNamingConvention();
            });
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();

        // var migrationSql = await File.ReadAllTextAsync("db_migration.sql");
        //
        // await _dbContainer.ExecScriptAsync(migrationSql);
    }

    public new Task DisposeAsync()
    {
        //_dbContainer.DisposeAsync();
        return _dbContainer.StopAsync();
    }
}