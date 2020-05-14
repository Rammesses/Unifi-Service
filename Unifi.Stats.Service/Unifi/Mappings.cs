using System;
using System.Collections.Generic;
using System.Text;
using Unifi.Stats.Service.Unifi.Model;
using UniFiSharp.Json;

namespace Unifi.Stats.Service.Unifi
{
    internal static class Mappings
    {
        internal static UnifiNetworkDevice AsUnifiNetworkDevice(this JsonNetworkDevice deviceData)
        {
            return new UnifiNetworkDevice
            { 
                Name = deviceData.name
            };
        }

        internal static UnifiNetworkClient AsUnifiNetworkClient(this JsonClient clientData)
        {
            return new UnifiNetworkClient
            {
                Name = clientData.name,
                IpAddress = clientData.ip
            };
        }
    }
}
