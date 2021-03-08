using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefArk.Car.Models;

namespace RefArk.Car.Services
{
    public class SimulateDrivingService : IHostedService, IDisposable
    {
        private int executionCount = 0;
        private readonly ILogger<SimulateDrivingService> _logger;
        private Timer _timer;

        public SimulateDrivingService(ILogger<SimulateDrivingService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Simulate Driving Service running.");

            return Task.CompletedTask;
        }

        public Task StartDrivingAsync()
        {
            _logger.LogInformation("Start driving...");

            _timer = new Timer(DriveAsync, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            return Task.CompletedTask;
        }

        private async void DriveAsync(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            _logger.LogInformation("Driving... Count: {Count}", count);

            var eventHubConnectionString = "Endpoint=sb://okq8-ark-eventhub.servicebus.windows.net/;SharedAccessKeyName=okq8-ark;SharedAccessKey=DxHhESlasbUVHbIVG9apA5F9hdmeDamr90xk7GOYKZU=";
            var eventHubName = "car-waypoint";

            var producer = new EventHubProducerClient(eventHubConnectionString, eventHubName);
            string[] partitionIds = await producer.GetPartitionIdsAsync();

            try
            {
                using EventDataBatch eventBatch = await producer.CreateBatchAsync();

                for (var counter = 0; counter < 10; ++counter)
                {
                    Bogus.Faker faker = new Bogus.Faker();
                    var lat = faker.Address.Latitude().ToString();
                    var lon = faker.Address.Latitude().ToString();
                    var timestamp = DateTime.Now;
                    var waypoint = new WaypointModel { WaypointID=Guid.NewGuid(), CarID = "1", Latitude = lat, Longitude = lon, Timestamp = timestamp };
                    var body = JsonSerializer.Serialize(waypoint);
                    _logger.LogInformation(body);

                    var eventBody = new BinaryData(body);
                    var eventData = new EventData(eventBody);
                    
                    eventBatch.TryAdd(eventData);
                }
                await producer.SendAsync(eventBatch);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: {0}", ex.Message);
            }
            finally
            {
                await producer.CloseAsync();
            }

        }

        private async void SendTripEndedEvent()
        {
            var connectionString = "Endpoint=sb://okq8-ark.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=NXvRQ3vev/pvuTSfU5c0lPg2jTVWT8lN52HB4ZOBHhA=";
            var topicName = "tripended";
            
            // create a Service Bus client 
            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(topicName);

                TripEndedEventModel tripEndedEvent = new TripEndedEventModel() { CarID = "1", Timestamp = DateTime.Now };
                var body = JsonSerializer.Serialize(tripEndedEvent);

                ServiceBusMessage message = new ServiceBusMessage(body);
                message.ContentType = "application/json"; 

                await sender.SendMessageAsync(message);

                _logger.LogInformation($"Sent a single message to the queue: {topicName}");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Simulate Driving Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);
            SendTripEndedEvent();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
