using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WebAPI.Filters;

namespace WebAPI.Extensions
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            services.AddSwaggerGen(c =>
             {
                 c.UseInlineDefinitionsForEnums();
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API" });
                 c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                 c.AddSecurityRequirement(new OpenApiSecurityRequirement
                     {
                         { securityScheme, new string[] { } }
                     });
                 c.OperationFilter<SwaggerFilter>();
                 c.SchemaFilter<SwaggerIgnoreFilter>();
             });
            return services;
        }


    }
}
