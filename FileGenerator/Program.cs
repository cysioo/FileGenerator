using FileGenerator;
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
        services.AddSingleton<IStringGenerator, StringGenerator>();
        services.AddTransient<IFileService, FileService>();
    })
    .Build();

var app = host.Services.GetRequiredService<ApplicationRunner>();
app.Run();