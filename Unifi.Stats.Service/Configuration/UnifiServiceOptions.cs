using System;

namespace Unifi.Stats.Service.Configuration
{
    public class UnifiServiceOptions
    {
        private static TimeSpan DefaultPollingInterval = TimeSpan.FromSeconds(30);

        public TimeSpan PollingInterval { get; set; } = DefaultPollingInterval;
    }
}