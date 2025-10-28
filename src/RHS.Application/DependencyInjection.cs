using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RHS.Application.Data;

namespace RHS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IDispatcher>(d => new Dispatcher(d.GetService<IMediator>()));
    }
}