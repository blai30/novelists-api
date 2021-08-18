using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NovelistsApi.Domain;
using NovelistsApi.Infrastructure;
using Serilog;

Console.WriteLine(DateTime.UtcNow.ToString("R"));
Console.WriteLine(Environment.ProcessId);

/*
 * Create app builder and inject services and configuration.
 */

var builder = WebApplication.CreateBuilder(args);

// Initialize Serilog logger from appsettings.json configurations.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddOptions();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("Docker"));

// I added this AddRouting line. Should go above AddControllers.
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

// Planned support for snake_case in .NET 6.
builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.PropertyNamingPolicy = new JsonSnakeCaseNamingPolicy());

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Novelists-Api", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName);
});

/*
 * Build the app.
 */

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Novelists-Api WebApi v1"));
}

// TODO: Global CORS policy to allow any origin with credentials.
app.UseCors(policyBuilder => policyBuilder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
