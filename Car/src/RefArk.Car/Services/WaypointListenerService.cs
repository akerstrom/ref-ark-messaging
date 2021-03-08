using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefArk.Car.Processor;
using RefArk.Car.Models;

namespace RefArk.Car.Services
{
    public class WaypointListenerService : IHostedService, IDisposable
    {
        private readonly ILogger<WaypointListenerService> _logger;
        private WaypointProcessor _processor;

        public WaypointListenerService(ILogger<WaypointListenerService> logger, WaypointProcessor processor)
        {
            _logger = logger;
            _processor = processor;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waypoint Listener Service running.");

            return Task.CompletedTask;
        }

        public Task StartListening()
        {
            _logger.LogInformation("Start listening...");
            _processor.StartProcessing();

            return Task.CompletedTask;
        }

        public Task StopListening()
        {
            _logger.LogInformation("Stop listening...");
            _processor.StopProcessing();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waypoint Listener Service is stopping.");

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }


    }
}
