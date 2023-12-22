using OriginalLanguage.EmailWorker.Configuration;
using OriginalLanguages.EmailWorker;

var builder = WebApplication.CreateBuilder(args);
builder.AddAppLogger();

// Add services to the container.

var services = builder.Services;

services.AddAppServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.Run();
