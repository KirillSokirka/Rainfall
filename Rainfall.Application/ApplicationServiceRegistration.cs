using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Rainfall.Application.Contracts.Services;
using Rainfall.Application.Services;

namespace Rainfall.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IRainfallDataService, RainfallDataService>();
        
        return services;
    }
}