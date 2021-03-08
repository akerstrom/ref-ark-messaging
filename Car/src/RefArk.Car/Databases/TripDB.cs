using RefArk.Car.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Car.Databases
{
    public class TripDB
    {
        public void Store(TripEndedEventModel trip)
        {
            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "okq8-ark-db.database.windows.net";
                builder.UserID = "peter";
                builder.Password = "";
                builder.InitialCatalog = "RefArk.Car.DB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    var sql = $"insert Trips(TripID, CarID, Timestamp) values('{ Guid.NewGuid() }', '{ trip.CarID }', '{ trip.Timestamp }')";
                    
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
    }
}
