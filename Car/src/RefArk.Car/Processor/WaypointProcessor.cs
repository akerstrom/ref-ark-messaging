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
using RefArk.Customer.Models;

namespace RefArk.Car.Processor
{
    public class WaypointProcessor
    {
        private readonly ILogger<WaypointProcessor> _logger;
        private EventProcessorClient processor;

        public WaypointProcessor(ILogger<WaypointProcessor> logger)
        {
            _logger = logger;
        }
        public Task StartProcessing()
        {
            _logger.LogInformation("Start listening...");
            var eventHubConnectionString = "Endpoint=sb://okq8-ark-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=/xRcZ2tUWKIO3h7DL4mvn2R0cRdkThLIJbHPJVCOSyM=";
            var eventHubName = "car-waypoint";
            var blobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=okq8arkstorage;AccountKey=GT9tDqzYqeYLDYUCxxQyt3nNDpgBB0YcngdGXWKSLkiWDfFtLYtH7kPW3UfZUuIsJCxFotUHRq4DL9nxaL6Jyg==;EndpointSuffix=core.windows.net";
            var blobContainerName = "car-waypoint";
            var consumerGroup = "StoreData";

            BlobContainerClient storageClient = new BlobContainerClient(blobStorageConnectionString, blobContainerName);
            processor = new EventProcessorClient(storageClient, consumerGroup, eventHubConnectionString, eventHubName);

            processor.ProcessEventAsync += ProcessEventHandler;
            processor.ProcessErrorAsync += ProcessErrorHandler;

            // Start the processing
            processor.StartProcessing();

            return Task.CompletedTask;
        }

        public Task StopProcessing()
        {
            processor.StopProcessing();
            return Task.CompletedTask;
        }

        async Task ProcessEventHandler(ProcessEventArgs eventArgs)
        {
            // Write the body of the event to the console window
            _logger.LogInformation("\tReceived event: {0}", Encoding.UTF8.GetString(eventArgs.Data.Body.ToArray()));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await eventArgs.UpdateCheckpointAsync(eventArgs.CancellationToken);
        }

        Task ProcessErrorHandler(ProcessErrorEventArgs eventArgs)
        {
            // Write details about the error to the console window
            _logger.LogInformation($"\tPartition '{ eventArgs.PartitionId}': an unhandled exception was encountered. This was not expected to happen.");
            _logger.LogInformation(eventArgs.Exception.Message);
            return Task.CompletedTask;
        }
    }

}
