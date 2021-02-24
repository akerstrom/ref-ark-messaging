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
using RefArk.Customer.Models;

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
            var eventHubConnectionString = "Endpoint=sb://okq8-ark-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/xRcZ2tUWKIO3h7DL4mvn2R0cRdkThLIJbHPJVCOSyM=";
            var eventHubName = "car-waypoint";
            var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=okq8arkstorage;AccountKey=GT9tDqzYqeYLDYUCxxQyt3nNDpgBB0YcngdGXWKSLkiWDfFtLYtH7kPW3UfZUuIsJCxFotUHRq4DL9nxaL6Jyg==;EndpointSuffix=core.windows.net";
            var blobContainerName = "car-waypoint";
            var consumerGroup = "StoreData";

            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            EventProcessorClient processor = new EventProcessorClient(storageClient, consumerGroup, eventHubConnectionString, eventHubName);

            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            processor.StartProcessing();

            return Task.CompletedTask;
        }

        public Task StopListening()
        {
            _logger.LogInformation("Stop listening...");
            processor.StopProcessing();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waypoint Listener Service is stopping.");
            processor.StopProcessing();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }


    }
}
