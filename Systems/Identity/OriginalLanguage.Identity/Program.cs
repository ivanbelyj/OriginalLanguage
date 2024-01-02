using OriginalLanguage.Context;
using OriginalLanguage.Identity.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();

// Add services to the container.

IServiceCollection services = builder.Services;
services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppIS4Configuration();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAppCors();
app.UseAppHealthChecks();
app.UseAppIS4();

app.Run();
