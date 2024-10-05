using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Infrastructure.Database;
using StackLab.Survey.Infrastructure.Services;

namespace StackLab.Survey.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"), 
                    new MySqlServerVersion(new Version(8, 0))
                )
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
              );

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        services.AddSingleton<INotificationService, NotificationService>();

        return services;
    }
}
