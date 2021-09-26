namespace ChatSystem.Infrastructure.ConfigurationSettings
{
    using Microsoft.Extensions.Configuration;

    internal class RabbitMQSettings
    {
        private const string Key = "rabbitMQ"; 
        private const string HostKey = "host";
        private const string PortKey = "port";
        private const string UsernameKey = "username";
        private const string PasswordKey = "password";

        public RabbitMQSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(Key);
            Host = settings.GetStringConfigurationValue(HostKey);
            Port = settings.GetIntConfigurationValue(PortKey);
            Username = settings.GetStringConfigurationValue(UsernameKey);
            Password = settings.GetStringConfigurationValue(PasswordKey);
        }

        public string Host { get; }

        public int Port { get; }

        public string Username { get; }

        public string Password { get; }
    }
}