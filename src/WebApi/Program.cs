using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NovelistsApi;
using Serilog;

Console.WriteLine(DateTime.UtcNow.ToString("R"));
Console.WriteLine(Environment.ProcessId);

var host = Host
    .CreateDefaultBuilder(args)
    .UseSerilog()
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build();

// Configure services.
// using var scope = host.Services.CreateScope();
// var services = scope.ServiceProvider;

await host.RunAsync();
