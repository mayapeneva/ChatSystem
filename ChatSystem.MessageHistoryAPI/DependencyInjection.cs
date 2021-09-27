namespace ChatSystem.MessageHistoryAPI
{
    using ChatSystem.MessageHistoryAPI.Contracts;
    using ChatSystem.MessageHistoryAPI.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection RegisterMessageHistoryService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMessageHistoryService, MessageHistoryService>();
            return services;
        }
    }
}
