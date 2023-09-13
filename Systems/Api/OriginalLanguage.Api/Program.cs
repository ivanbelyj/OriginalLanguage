using OriginalLanguage.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
