using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

using MediatR;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Unifi.Stats.Service.Configuration;
using Timer = System.Timers.Timer;

namespace Unifi.Stats.Service
{
    public class Worker : IHostedService, IDisposable
    {
        private readonly IMediator mediator;
        private readonly UnifiServiceOptions options;
        private readonly ILogger<Worker> logger;
        private Timer timer;

        public Worker(
            IMediator mediator,
            IOptions<UnifiServiceOptions> options,
            ILogger<Worker> logger)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            this.logger.LogTrace($"{nameof(Worker)} is starting...");

            this.timer = new Timer(options.PollingInterval.TotalMilliseconds)
            {
                AutoReset = true,
            };

            this.timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            this.timer.Start();

            this.logger.LogInformation($"{nameof(Worker)} is started.");

            await this.mediator.Publish(new RetrieveUnifiStatsCommand());
        }

        private async void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            this.logger.LogInformation($"{nameof(Worker)} triggered.");
            await this.mediator.Publish(new RetrieveUnifiStatsCommand());
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogTrace($"{nameof(Worker)} is stopping...");

            this.timer.Stop();

            this.logger.LogInformation($"{nameof(Worker)} is stopped.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}
