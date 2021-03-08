using RefArk.Station.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Station.Databases
{
    public class StationDB
    {
        public void StoreIncomingCar(TripEndedEventModel trip)
        {
            // Get Customer from RefArk.Customer
            // Call ​/api/Customer/ByCar/{carID}
            var customerID = "";

            // Find Station based on Position
            var stationID = FindStation(trip.StartPosition, trip.EndPosition);

            //
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "okq8-ark-db.database.windows.net";
                builder.UserID = "peter";
                builder.Password = "";
                builder.InitialCatalog = "RefArk.Customer.DB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    var sql = $"insert IncomingCars(StationID, CustomerID, CarID, FuelLevel, WiperFluidLevel, Timestamp) values('{stationID}', '{ customerID }', '{ trip.CarID }', '{trip.FuelLevel}', '{trip.WiperFluidLevel}', '{ trip.Timestamp }')";

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

        private string FindStation(Position startPosition, Position endPosition)
        {
            return "1";
        }
    }
}
