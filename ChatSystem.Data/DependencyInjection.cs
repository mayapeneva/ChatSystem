namespace ChatSystem.Data
{
    using Infrastructure.ConfigurationSettings;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings(configuration);
            configuration.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);
            services.AddSingleton(databaseSettings);
            services.AddDbContext<ChatSystemContext>(options => options.UseNpgsql(new DatabaseSettings(configuration).GetConnectionString()));
            return services;
        }
    }
}
