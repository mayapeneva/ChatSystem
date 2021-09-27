namespace ChatSystem.Infrastructure.ConfigurationSettings
{
    using Microsoft.Extensions.Configuration;

    public class IntervalSettings
    {
        private const string Key = "intervalSettings";
        private const string DayKey = "day";
        private const string HourKey = "hour";
        private const string MinuteKey = "minute";
        private const string SecondKey = "second";

        public IntervalSettings(IConfiguration configuration)
        {
            var settings = configuration.GetSection(Key);
            Day = settings.GetIntConfigurationValue(DayKey);
            Hour = settings.GetIntConfigurationValue(HourKey);
            Minute = settings.GetIntConfigurationValue(MinuteKey);
            Second = settings.GetIntConfigurationValue(SecondKey);

        }

        public int Day { get; }

        public int Hour { get; }

        public int Minute { get; }

        public int Second { get; }
    }
}
