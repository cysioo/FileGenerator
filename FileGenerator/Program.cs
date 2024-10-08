﻿using FileGenerator;
using FileGenerator.LineGeneration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, configuration) =>
    {
        configuration.Sources.Clear();
        configuration.AddJsonFile("appsettings.json", optional: false);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IAppSettings>(s => new AppSettings(hostContext.Configuration));
        services.AddSingleton<ApplicationRunner>();
        services.AddTransient<IFileService, FileService>();
        services.AddSingleton<ILineGenerator, LineGenerator>();
    })
    .Build();

var app = host.Services.GetRequiredService<ApplicationRunner>();
app.Run();