using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RHS.Application.Data;

namespace RHS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
        services.AddScoped<IDispatcher, Dispatcher>();
        services.AddScoped<IDispatcher>(d => new Dispatcher(d.GetService<IMediator>()));
        
        return services;
    }
}