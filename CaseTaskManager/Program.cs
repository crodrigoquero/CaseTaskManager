using Microsoft.OpenApi.Models;
using CaseTaskManager.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CaseTaskManager API",
        Version = "v1"
    });

    c.CustomSchemaIds(t => t.FullName!.Replace("+", "."));

});

builder.Services.AddApplicationServices(); // DI registrations

var app = builder.Build();

app.UseApplicationDefaults();
app.Run();
