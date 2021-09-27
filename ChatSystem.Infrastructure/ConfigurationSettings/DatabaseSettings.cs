namespace ChatSystem.Infrastructure.ConfigurationSettings
{
    using Microsoft.Extensions.Configuration;

    public class DatabaseSettings
    {
        public const string Key = "postgres";
        private const string HostKey = "host";
        private const string PortKey = "port";
        private const string UsernameKey = "username";
        private const string PasswordKey = "password";
        public const string NameKey = "name";

        public DatabaseSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(Key);
            Host = settings.GetStringConfigurationValue(HostKey);
            Port = settings.GetIntConfigurationValue(PortKey);
            Username = settings.GetStringConfigurationValue(UsernameKey);
            Password = settings.GetStringConfigurationValue(PasswordKey);
            Name = settings.GetStringConfigurationValue(NameKey);
        }

        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }

        public string Name { get; }

        public string GetConnectionString() => "Server=.\\SQLExpress;Database=chatsystem-db;Trusted_Connection=True";
        //$"Data Source=.\\SQLExpress;Initial Catalog={this.Name};User Id={this.Username};Password={this.Password};";
        //$"Host={this.Host};Port={this.Port};Database={this.Name};Username={this.Username};Password={this.Password}";
        //
    }
}
