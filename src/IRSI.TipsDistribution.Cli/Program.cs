using IRSI.CommonTools;
using IRSI.CommonTools.Abstractions;
using IRSI.CommonTools.Abstractions.FileSystem;
using IRSI.CommonTools.FileSystem;
using IRSI.TipsDistribution.Application;
using IRSI.TipsDistribution.Application.Contracts;
using IRSI.TipsDistribution.Application.Options;
using IRSI.TipsDistribution.Cli;
using IRSI.TipsDistribution.Cli.Commands.Distribute;
using IRSI.TipsDistribution.Cli.Commands.Tasks;
using IRSI.TipsDistribution.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SpectreConsole;
using Spectre.Console.Cli;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.SpectreConsole()
    .CreateBootstrapLogger();

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.Configure<StoreSettings>(context.Configuration.GetSection(StoreSettings.SectionName));
        services.Configure<TaskSettings>(context.Configuration.GetSection(TaskSettings.SectionName));

        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));

        services.AddHttpClient<ICreciPayHttpClient, CreciPayHttpClient>((provider, client) =>
        {
            client.BaseAddress = new("https://rpc.serv.crecipay.com");
        });

        services.AddTransient<IEnvironment, EnvironmentWrapper>();
        services.AddTransient<IFileSystem, FileSystemWrapper>();
        services.AddTransient<IDateOnly, DateOnlyWrapper>();

        services.AddTransient<IProcess, ProcessWrapper>();
    })
    .UseSerilog((context, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .WriteTo.SpectreConsole();
    });

var register = new TypeRegistrar(builder);
var app = new CommandApp(register);

app.Configure(config =>
{
    config.AddBranch("distribute", distribute => { distribute.AddCommand<DistributeCreciPayCommand>("crecipay"); });
    config.AddBranch("tasks", tasks =>
    {
        tasks.HideBranch();

        tasks.AddCommand<InstallTaskSchedulerTasksCommand>("install");
        tasks.AddCommand<UnInstallTaskSchedulerTasksCommand>("uninstall");
    });
});

await app.RunAsync(args);