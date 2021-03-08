using RefArk.Customer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RefArk.Customer.Databases
{
    public class CustomerDB
    {
        Dictionary<string, string> carid_customerid_map;
        public CustomerDB()
        {
            // mapping between Car and Customer
            carid_customerid_map = new Dictionary<string, string>();
            carid_customerid_map.Add("1", "7A360160-7D26-469B-8FEC-B04AD710331F");
        }

        public void UpdateCustomer(TripEndedEventModel trip)
        {

            try
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "okq8-ark-db.database.windows.net";
                builder.UserID = "peter";
                builder.Password = "Changeme2012";
                builder.InitialCatalog = "RefArk.Customer.DB";

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    var customerID = carid_customerid_map[trip.CarID];

                    var sql = $"update Customers set NoOfTrips = NoOfTrips + 1 where CustomerID = '{ customerID }'";

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

        public CustomerModel GetCustomerByCarID(string carID)
        {
            var customerID = carid_customerid_map[carID];

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = "okq8-ark-db.database.windows.net";
            builder.UserID = "peter";
            builder.Password = "";
            builder.InitialCatalog = "RefArk.Customer.DB";

            using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
            {
                connection.Open();

                var sql = $"select CustomerID, Name, NoOfTrips from Customers where CustomerID = '{ customerID }'";

                var customer = new CustomerModel();
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        customer.CustomerID = reader[0].ToString();
                        customer.Name = reader[1].ToString();
                        customer.NoOfTrips = int.Parse(reader[2].ToString());
                        break;
                    }
                }
                return customer;
            }
        }
    }
}
