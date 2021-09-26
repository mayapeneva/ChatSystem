namespace ChatSystem.Infrastructure.Configurations
{
    using System;
    using Microsoft.Extensions.Configuration;

    internal static class ConfigurationHelper
    {
        public static string GetStringConfigurationValue(this IConfigurationSection configurationSection, string key)
        {
            var value = configurationSection[key];
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception(
                    $"There is no key {key} defined in configuration section {configurationSection.Key}.");
            }

            return value;
        }

        public static int GetIntConfigurationValue(this IConfigurationSection configurationSection, string key)
        {
            var value = configurationSection.GetStringConfigurationValue(key);
            if (!int.TryParse(value, out int parsed))
            {
                throw new Exception($"{key} defined in configuration section {configurationSection.Key} must be an integer.");
            }

            return parsed;
        }
    }
}
