using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Unifi.Stats.Service.Unifi;

namespace Unifi.Stats.Service.Handlers
{
    public class RetrieveUnifiStatsCommandHandler : INotificationHandler<RetrieveUnifiStatsCommand>
    {
        private readonly IUnifiClient unifiClient;
        private readonly ILogger logger;
        
        public RetrieveUnifiStatsCommandHandler(IUnifiClient client, ILogger<RetrieveUnifiStatsCommandHandler> logger)
        {
            this.unifiClient = client ?? throw new ArgumentNullException(nameof(client));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(RetrieveUnifiStatsCommand notification, CancellationToken cancellationToken)
        {
            this.logger.LogInformation($"Getting statistics from {this.unifiClient}");
            
            var devices = (await this.unifiClient.GetDevicesAsync()).ToList();

            this.logger.LogInformation($"Found {devices.Count} devices");

            foreach(var device in devices)
            {
                this.logger.LogInformation($"- '{device.Name}'");
            }

            var clients = (await this.unifiClient.GetClientsAsync()).ToList();

            this.logger.LogInformation($"Found {clients.Count} clients");

            foreach (var client in clients)
            {
                this.logger.LogInformation($"- '{client.Name}' ({client.IpAddress})");
            }

        }
    }
}
