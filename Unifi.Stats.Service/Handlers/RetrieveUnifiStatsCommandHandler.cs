using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Unifi.Stats.Service.Unifi;

namespace Unifi.Stats.Service.Handlers
{
    public class RetrieveUnifiStatsCommandHandler : INotificationHandler<RetrieveUnifiStatsCommand>
    {
        private readonly IUnifiClient unifiClient;

        public RetrieveUnifiStatsCommandHandler(IUnifiClient client)
        {
            this.unifiClient = client;
        }

        public async Task Handle(RetrieveUnifiStatsCommand notification, CancellationToken cancellationToken)
        {
            var devices = (await this.unifiClient.GetDevicesAsync()).ToList();

            Console.WriteLine($"Found {devices.Count} devices");

            foreach(var device in devices)
            {
                Console.WriteLine($"- '{device.Name}'");
            }

            var clients = (await this.unifiClient.GetClientsAsync()).ToList();

            Console.WriteLine($"Found {clients.Count} clients");

            foreach (var client in clients)
            {
                Console.WriteLine($"- '{client.Name}' ({client.IpAddress})");
            }

        }
    }
}
