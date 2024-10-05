using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StackLab.Survey.Application.Common.Behaviours;
using System.Reflection;

namespace StackLab.Survey.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));

        });

        return services;
    }
}
