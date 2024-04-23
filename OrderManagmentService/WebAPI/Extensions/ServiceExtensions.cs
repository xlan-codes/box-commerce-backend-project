using Application;
using Application.Behaviors;
using Application.Contracts;
using Application.Contracts.Persistence;
using Application.Generics.Dtos.Settings;
using Application.Generics.Interfaces;
using Application.Generics.Services;
using FluentValidation;
using Infrastructure.EntityConfigurations;
using Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
using Application.Schedulers;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Schedulers;
using WebAPI.Models;

namespace WebAPI.Extensions
{
    public static class DependencyExtensions
    {
        private static Assembly _applicationAssembly { get { return typeof(AssemblyReference).Assembly; } }

        #region Packages Configuration
        public static IServiceCollection AddConfigureExternalPackages(this IServiceCollection services)
        {
            services.AddMediatR(_applicationAssembly);
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddAutoMapper(_applicationAssembly);
            return services;
        }
        #endregion

        #region MongoDB Configuration
        public static IServiceCollection AddMongoDb(this IServiceCollection services, ConfigurationManager configuration)
        {
            MongoDBSchemaConfiguration.Configure();
            services.Configure<MongoDbSettings>(configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoRepository, MongoRepository>();
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
            return services;
        }
        #endregion

        #region Application Dependencies
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddSingleton<IOrderRepository, OrderRepository>();
            services.AddSingleton<IProductionScheduler, ProductionScheduler>();
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddAutoMapper(_applicationAssembly);
            services.AddValidatorsFromAssembly(_applicationAssembly);
            return services;
        }
        #endregion

        #region Environment Variables
        public static IServiceCollection AddEnvironmentVariablesDependencies(this IServiceCollection services, ConfigurationManager configuration)
        {
            // services.Configure<CISL_AZTSettings>(configuration.GetSection(CISL_AZTSettings.SettingsSection));
            services.Configure<OIDCSettings>(configuration.GetSection(OIDCSettings.SettingsSection));
            services.Configure<UrlSettings>(configuration.GetSection(UrlSettings.SettingsSection));
            return services;
        }
        #endregion

    }
}
