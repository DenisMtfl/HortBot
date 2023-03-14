using HortBot;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSystemd()
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddHostedService<Worker>();
        services.Configure<Config>(configuration.GetSection("Config"));
        services.AddWindowsService(opt => opt.ServiceName = "HortBot");
    })

    .Build();

host.Run();