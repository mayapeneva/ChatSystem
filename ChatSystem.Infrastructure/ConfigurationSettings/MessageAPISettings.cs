namespace ChatSystem.Infrastructure.ConfigurationSettings
{
    using Microsoft.Extensions.Configuration;

    public class MessageAPISettings
    {
        private const string Key = "messageAPI";
        private const string EndpointKey = "endpoint";

        public MessageAPISettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(Key);
            Endpoint = settings.GetStringConfigurationValue(EndpointKey);
        }

        public string Endpoint { get; }
    }
}
