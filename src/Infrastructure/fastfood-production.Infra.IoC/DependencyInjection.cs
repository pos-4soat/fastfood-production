using fastfood_production.Application.Shared.BaseResponse;
using fastfood_production.Domain.Contracts.Http;
using fastfood_production.Domain.Contracts.Repository;
using fastfood_production.Infra.Http;
using fastfood_production.Infra.SqlServer.Context;
using fastfood_production.Infra.SqlServer.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Infra.IoC;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureBehavior();
        services.ConfigureServices();
        services.ConfigureAutomapper();
        services.ConfigureMediatr();
        services.ConfigureFluentValidation();
        services.ConfigureHttpClient(configuration);
        services.ConfigureDatabase(configuration);
    }

    private static void ConfigureBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
    }
    private static void ConfigureAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Result).Assembly);
    }

    private static void ConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Result).Assembly));
    }

    private static void ConfigureFluentValidation(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<Result>();
        service.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        service.AddFluentValidationRulesToSwagger();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IProductionRepository, ProductionRepository>();
    }

    private static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
    {
        IConfiguration externalConfig = configuration.GetSection("Http");
        string baseUrl = externalConfig.GetSection("Order").Value;

        services.AddTransient<IOrderHttpClient>(provider =>
        {
            string? baseAddress = baseUrl;
            return new OrderHttpClient(baseAddress);
        });
    }

    private static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                                                     b => b.MigrationsAssembly("fastfood-production.Infra.SqlServer")).LogTo(s => Console.WriteLine(s)));
    }
}
