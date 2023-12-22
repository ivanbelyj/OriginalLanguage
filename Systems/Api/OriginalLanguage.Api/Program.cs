using OriginalLanguage.Api;
using OriginalLanguage.Api.Configuration;
using OriginalLanguage.Api.Middlewares;
using OriginalLanguage.Context;
using OriginalLanguage.Context.Setup;
using OriginalLanguage.Services.Settings;
using OriginalLanguage.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

MainSettings? mainSettings = ConfigurationUtils.Load<MainSettings>("Main",
    builder.Configuration);
OpenApiSettings? openApiSettings = ConfigurationUtils.Load<OpenApiSettings>("OpenApi",
    builder.Configuration);
IdentitySettings? identitySettings = ConfigurationUtils.Load<IdentitySettings>("Identity",
    builder.Configuration);
ArgumentNullException.ThrowIfNull(openApiSettings);
ArgumentNullException.ThrowIfNull(identitySettings);


var services = builder.Services;

// Add services to the container.
services.AddAppHealthChecks();
services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);
services.AddAppAuth(identitySettings);

services.AddAppCors();
services.AddAppVersioning();
services.AddAppOpenApi(openApiSettings, identitySettings);
services.AddAppAutoMapper();
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
app.UseAppAuth();

app.UseStaticFiles();

app.UseAppControllersAndViews();

app.UseMiddleware<ExceptionsMiddleware>();

app.Run();
