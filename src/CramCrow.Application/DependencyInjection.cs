using CramCrow.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CramCrow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CardService>();

        return services;
    }
}