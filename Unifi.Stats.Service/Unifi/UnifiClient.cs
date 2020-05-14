using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Unifi.Stats.Service.Unifi.Model;
using UniFiSharp;

namespace Unifi.Stats.Service.Unifi
{
    public class UnifiClient : IUnifiClient
    {
        private readonly IOptions<UnifiClientOptions> options;
        private readonly UniFiApi innerClient;

        public UnifiClient(UniFiApi innerClient, IOptions<UnifiClientOptions> options)
        {
            this.options = options;
            this.innerClient = innerClient;
        }

        public UnifiClientOptions Options => this.options.Value;

        public bool IsAuthenticated { get; private set; }

        internal UniFiApi InnerClient => this.innerClient;

        public async Task<IEnumerable<UnifiNetworkClient>> GetClientsAsync()
        {
            await EnsureAuthenticated();

            var clientsData = await this.InnerClient.ClientList();
            return clientsData.Select(d => d.AsUnifiNetworkClient());
        }

        public async Task<IEnumerable<UnifiNetworkDevice>> GetDevicesAsync()
        {
            await EnsureAuthenticated();

            var devicesData = await this.InnerClient.NetworkDeviceList();
            return devicesData.Select(d => d.AsUnifiNetworkDevice());
        }

        private async Task EnsureAuthenticated()
        {
            if (this.IsAuthenticated)
            {
                return;
            }

            await this.InnerClient.Authenticate();

            this.IsAuthenticated = true;
        }
    }
}