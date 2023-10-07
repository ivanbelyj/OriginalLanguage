using OriginalLanguage.Api;
using OriginalLanguage.Api.Configuration;
using OriginalLanguage.Api.Middlewares;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Setup;
using OriginalLanguage.Services.Settings;
using OriginalLanguage.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

MainSettings? mainSettings = Configuration.Load<MainSettings>("Main",
    builder.Configuration);
OpenApiSettings? openApiSettings = Configuration.Load<OpenApiSettings>("OpenApi",
    builder.Configuration);
ArgumentNullException.ThrowIfNull(openApiSettings);


var services = builder.Services;

// Add services to the container.
services.AddAppHealthChecks();
services.AddHttpContextAccessor();
services.AddAppDbContext(builder.Configuration);
services.AddAppCors();
services.AddAppVersioning();
services.AddAppOpenApi(openApiSettings);
services.AddAppAutoMappers();
services.AddAppControllersAndViews();

services.AddAppServices();

var app = builder.Build();

DbInitializer.Initialize(app.Services);
DbSeeder.SeedDb(app.Services, true);

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.UseAppHealthChecks();

app.UseAppCors();

app.UseAppOpenApi();

app.UseStaticFiles();

app.UseAppControllersAndViews();

app.UseMiddleware<ExceptionsMiddleware>();

app.Run();
