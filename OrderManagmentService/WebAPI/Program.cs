using Application.Generics.Interfaces;
using Application.Schedulers;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using Serilog;
using WebAPI.Extensions;
using WebAPI.Middlewares;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, config) => config.ReadFrom.Configuration(context.Configuration).ReadFrom.Services(services).Enrich.FromLogContext());

// Add services to the container.

builder.Services.AddApplicationDependencies();
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEnvironmentVariablesDependencies(builder.Configuration);
builder.Services.ConfigureSwagger();
builder.Services.AddRazorPages();
builder.Services.AddControllers();
string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy => { policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod(); });
});
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddConfigureExternalPackages();
// builder.Services.AddScoped<IOrderRepository, OrderRepository>();
// builder.Services.AddScoped<IInventoryService, InventoryService>();
// builder.Services.AddScoped<IProductionScheduler, ProductionScheduler>();
var app = builder.Build();
if (!builder.Environment.IsProduction())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            swaggerDoc.Servers = new List<OpenApiServer> {
                new OpenApiServer { Url = builder.Configuration.GetSection("URLS").GetValue<string>("AbsoluteUrl") },
            };
        });
    });
    app.UseSwaggerUI(c =>
    {
        // c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
        c.SwaggerEndpoint($"{builder.Configuration.GetSection("URLS").GetValue<string>("AbsoluteUrl")}/swagger/v1/swagger.json", "API v1.0");
    });
}
app.UseMiddleware<RequestsLoggerMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseStaticFiles();

app.MapControllers();
app.UseRouting();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthentication();
app.UseAuthorization();

// app.UseCors(MyAllowSpecificOrigins);
app.Run();
