using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using RefArk.Car.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Car.Databases
{
    public class WaypointDB
    {
        private readonly ILogger<WaypointDB> _logger;

        // The Azure Cosmos DB endpoint for running this sample.
        private static readonly string CosmosEndpointUri = "AccountEndpoint=https://okq8-ark.documents.azure.com:443/;AccountKey=G2DB2ia0AaU6JXUCNGCiQE2ZxgDlqnlwIaVpADeMoj89o9TQEA61h99XNkqx23IyGdLKajFA2mnMvKvso5DOiw==;";

        // The primary key for the Azure Cosmos account.
        private static readonly string PrimaryKey = "WaypointID";

        // The Cosmos client instance
        private CosmosClient cosmosClient;

        // The database we will create
        private Database database;

        // The container we will create.
        private Container container;

        // The name of the database and container we will create
        private string databaseId = "okq8-ark-cvpdb";
        private string containerId = "car-waypoint";

        public WaypointDB(ILogger<WaypointDB> logger)
        {
            _logger = logger;

            //cosmosClient = new CosmosClient(CosmosEndpointUri, PrimaryKey, new CosmosClientOptions() { ApplicationName = "Car-Waypoint" });
            cosmosClient = new CosmosClient(CosmosEndpointUri);
            database = cosmosClient.GetDatabase(databaseId);
            container = database.GetContainer(containerId);
        }
        public async void Store(WaypointModel waypointModel)
        {
            ItemResponse<WaypointModel> waypointResponse = await container.CreateItemAsync<WaypointModel>(waypointModel);
        }
        public void Close()
        {
            cosmosClient.Dispose();
        }
    }
}
