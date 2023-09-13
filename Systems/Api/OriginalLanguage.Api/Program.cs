using OriginalLanguage.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

// Add services to the container.
services.AddAppHealthChecks();
services.AddHttpContextAccessor();
services.AddAppCors();
services.AddAppVersioning();
services.AddAppOpenApi();
services.AddAppControllersAndViews();

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
