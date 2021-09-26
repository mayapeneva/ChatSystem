namespace ChatSystem.Infrastructure.Configurations
{
    using ChatSystem.Infrastructure.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQSettings = new RabbitMQSettings(configuration);
            configuration.GetSection(nameof(RabbitMQSettings)).Bind(rabbitMQSettings);
            services.AddSingleton(rabbitMQSettings);

            services
                .AddSingleton<IMessenger, RabbitMQMessenger>();
                //.AddDbContext<ChatsystemContext>(
                //options => options.UseNpgsql(new DatabaseConfiguration(configuration).GetConnectionString()));

            return services;
        }
    }
}
