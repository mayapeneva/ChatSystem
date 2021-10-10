namespace ChatSystem.Data
{
    using Infrastructure.ConfigurationSettings;
    using Infrastructure.Contracts;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Services;

    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DatabaseSettings(configuration);
            configuration.GetSection(nameof(DatabaseSettings)).Bind(databaseSettings);
            services.AddSingleton(databaseSettings);
            services.AddDbContext<ChatSystemContext>(options =>
                options.UseSqlServer(new DatabaseSettings(configuration).GetConnectionString())
                .UseLazyLoadingProxies());
            //options.UseNpgsql(new DatabaseSettings(configuration).GetConnectionString()));
            //
            return services;
        }

        public static IServiceCollection RegisterMessageRepository(this IServiceCollection services)
        {
            services.AddScoped<IMessageRepository, MessageRepository>();
            return services;
        }
    }
}
