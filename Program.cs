using HortBot;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureAppConfiguration((hostingContext, configuration) =>
        configuration.AddEnvironmentVariables())
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddHostedService<Worker>();
        services.Configure<Config>(configuration.GetSection("Config"));
        services.AddWindowsService(opt => opt.ServiceName = "HortBot");
    })

    .Build();

host.Run();