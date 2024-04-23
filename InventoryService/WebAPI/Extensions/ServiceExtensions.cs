using Application;
using Application.Behaviors;
using Application.Contracts;
using Application.Contracts.Persistence;

using Application.Generics.Interfaces;
using Application.Generics.Services;

using FluentValidation;
using Infrastructure.EntityConfigurations;
using Infrastructure.Mongo;
using Infrastructure.Settings;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;
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
            services.AddScoped(typeof(IApiService<>), typeof(ApiService<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddSingleton<IInventoryService, InventoryService>();
            services.AddAutoMapper(_applicationAssembly);
            services.AddValidatorsFromAssembly(_applicationAssembly);
            return services;
        }
        #endregion

    }
}
