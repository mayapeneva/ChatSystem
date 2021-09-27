namespace ChatSystem.Infrastructure.ConfigurationSettings
{
    using Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection RegisterRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = new RabbitMQSettings(configuration);
            configuration.GetSection(nameof(RabbitMQSettings)).Bind(rabbitMQSettings);
            services.AddSingleton(rabbitMQSettings);
            services.AddSingleton<IMessenger, RabbitMQMessenger>();
            return services;
        }

        public static IServiceCollection RegisterIntervalSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var intervalSettings = new IntervalSettings(configuration);
            configuration.GetSection(nameof(IntervalSettings)).Bind(intervalSettings);
            services.AddSingleton(intervalSettings);
            return services;
        }
    }
}
