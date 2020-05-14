using System;

namespace Unifi.Stats.Service.Configuration
{
    public class UnifiServiceOptions
    {
        public static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(30);

        public TimeSpan PollingInterval { get; set; } = DefaultPollingInterval;
    }
}