namespace ChatSystem.Message.Importer
{
    using Data;
    using Infrastructure.ConfigurationSettings;
    using MessageHistoryAPI;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using System.Threading.Tasks;

    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile($"appsettings.local.json", optional: true);
                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                });

            builder
                .ConfigureServices((hostContext, services) =>
                    {
                        services
                            .AddLogging()
                            .RegisterIntervalSettings(hostContext.Configuration)
                            .RegisterRabbitMQ(hostContext.Configuration)
                            .RegisterDatabase(hostContext.Configuration)
                            .RegisterMessageRepository(hostContext.Configuration)
                            .AddHostedService<ApplicationService>();
                    });

            await builder.RunConsoleAsync();
        }
    }
}
