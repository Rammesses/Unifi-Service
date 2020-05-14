using System;
using System.Security;

namespace Unifi.Stats.Service.Unifi
{
    public class UnifiClientOptions
    {
        public Uri ControllerUri { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SiteName { get; set; }

        public bool IgnoreSslValidation { get; set; }
    }
}