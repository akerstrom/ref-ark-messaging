﻿using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using RefArk.Station.Databases;
using RefArk.Station.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RefArk.Station.Subscribers
{
    public class TripEndedEventSubscriber
    {
        private readonly ILogger<TripEndedEventSubscriber> _logger;
        private ServiceBusClient client;
        private ServiceBusProcessor processor;
        
        public TripEndedEventSubscriber(ILogger<TripEndedEventSubscriber> logger)
        {
            _logger = logger;
            InitSubscrriber();
        }
        private void InitSubscrriber()
        {
            var connectionString = "Endpoint=sb://okq8-ark.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=[key]";
            var topicName = "tripended";
            var subscriptionName = "RefArk.Station.1";

            client = new ServiceBusClient(connectionString);
                
            processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions());
            processor.ProcessMessageAsync += ProcessEvent;
            processor.ProcessErrorAsync += ErrorHandler;

        }
        public async Task<Task> StartSubscribing()
        {
            await processor.StartProcessingAsync();
            return Task.CompletedTask;
        }

        public async Task<Task> StopSubscribing()
        {
            await processor.StopProcessingAsync();
            return Task.CompletedTask;
        }
        private async Task ProcessEvent(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var trip = JsonSerializer.Deserialize<TripEndedEventModel>(body);

            _logger.LogInformation("Message received - CarID: {0} Timestamp: {1}", trip.CarID, trip.Timestamp.ToString());
            StationDB stationDB = new StationDB();
            stationDB.StoreIncomingCar(trip);

            await args.CompleteMessageAsync(args.Message);
        }
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            _logger.LogInformation("Exception: ", args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
