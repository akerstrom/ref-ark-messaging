using Newtonsoft.Json;
using RefArk.Station.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RefArk.Station.Databases
{
    public class StationDB
    {
        public async void StoreIncomingCar(TripEndedEventModel trip)
        {
            // Get Customer from RefArk.Customer
            var customer = await GetCustomerByCarIDAsync(trip.CarID);

            // Find Station based on Position
            var stationID = FindStationID(trip.StartPosition, trip.EndPosition);

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "okq8-ark-db.database.windows.net";
                builder.UserID = "peter";
                builder.Password = "OKQ8okq8";
                builder.InitialCatalog = "RefArk.Station.DB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    var sql = $"insert IncomingCars(StationID, CustomerID, CarID, FuelLevel, WiperFluidLevel, Timestamp) values('{ stationID }', '{ customer.CustomerID }', '{ trip.CarID }', '{trip.FuelLevel}', '{trip.WiperFluidLevel}', '{ trip.Timestamp }')";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        var res = command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private string FindStationID(Position startPosition, Position endPosition)
        {
            return "1";
        }

        public async Task<CustomerModel> GetCustomerByCarIDAsync(string carID)        
        {
            // Call ​/api/Customer/ByCar/{carID}
            HttpClient client = new HttpClient();

            // var _base = new Uri("http://localhost:61392");
            var _base = new Uri("http://20.56.192.6/");
        
            var _path = "/api/Customer/ByCar/1";
            var uri = new Uri(_base, _path);
            var res = await client.GetStringAsync(uri);
            var customer = JsonConvert.DeserializeObject<CustomerModel>(res);
            return customer;
        }
    }
}
