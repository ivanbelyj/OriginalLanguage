using OriginalLanguage.Api;
using OriginalLanguage.Api.Configuration;
using OriginalLanguage.Services.Settings;
using OriginalLanguage.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

MainSettings? mainSettings = Settings.Load<MainSettings>("Main",
    builder.Configuration);
OpenApiSettings? openApiSettings = Settings.Load<OpenApiSettings>("OpenApi",
    builder.Configuration);


var services = builder.Services;

// Add services to the container.
services.AddAppHealthChecks();
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppVersioning();
services.AddAppOpenApi(openApiSettings);
services.AddAppControllersAndViews();

services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();

app.UseAppHealthChecks();

app.UseAppCors();

app.UseAppSwagger();

app.UseStaticFiles();

app.UseAppControllersAndViews();

app.Run();
