using Serilog;
using StackLab.Survey.Application;
using StackLab.Survey.Application.Common.Interfaces;
using StackLab.Survey.Application.Common.Options;
using StackLab.Survey.Infrastructure;
using StackLab.Survey.WebAPI.Services;

namespace StackLab.Survey.WebAPI.Configuration;

public static class CommonConfiguration
{
    public static void ConfigureBuilder(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((hostContext, services, configuration) =>
        {
            configuration.ReadFrom.Configuration(builder.Configuration);
        });


        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddHttpContextAccessor();

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure(builder.Configuration);

        builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<ICurrentUser, CurrentUser>();
    }

    public static void ConfigureApp(WebApplication app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();
    }
}
