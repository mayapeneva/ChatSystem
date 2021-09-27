namespace ChatSystem.MessageHistoryAPI
{
    using Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public static class DependencyInjection
    {
        public static IServiceCollection RegisterMessageHistoryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}
