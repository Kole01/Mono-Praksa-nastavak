using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Test.Model;
using Test.Repository.Common;

namespace Test.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        public static string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);

        public Customer FindCustomerById(Guid id)
        {

            Customer foundCustomer = new Customer();
            string queryString = "SELECT [Id],[FirstName],[lastName],[Age] FROM [Prak].[dbo].[Customer] WHERE [Id]= @id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    foundCustomer.CustomerId = reader.GetGuid(0);
                    foundCustomer.CustomerFirstName = reader.GetString(1);
                    foundCustomer.CustomerLastName = reader.GetString(2);
                    foundCustomer.CustomerAge = reader.GetInt32(3);

                }
                conn.Close();
            }
            if (foundCustomer == null)
            {
                return null;
            }
            return foundCustomer;

        }

        public List<Customer> AllCustomers()
        {
            List<Customer> customers = new List<Customer>();
            string queryString = "SELECT * FROM [Prak].[dbo].[Customer]";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Customer tempCustomer = new Customer();
                tempCustomer.CustomerId = reader.GetGuid(0);
                tempCustomer.CustomerFirstName = reader.GetString(1);
                tempCustomer.CustomerLastName = reader.GetString(2);
                tempCustomer.CustomerAge = reader.GetInt32(4);
                customers.Add(tempCustomer);
            }
            conn.Close();
            if (customers != null)
            {
                return customers;
            }    
            return null;
        }

        public bool SaveCustomer(Customer newCustomer)
        {
            newCustomer.CustomerId = Guid.NewGuid();
            string queryString = "INSERT INTO Customer (Id,FirstName,LastName, Age) VALUES (@id,@CustomerFirstName,@CustomerLastName,@CustomerAge)";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@id", newCustomer.CustomerId);
            cmd.Parameters.AddWithValue("@CustomerFirstName", newCustomer.CustomerFirstName);
            cmd.Parameters.AddWithValue("@CustomerLastName", newCustomer.CustomerLastName);
            cmd.Parameters.AddWithValue("@CustomerAge", newCustomer.CustomerAge);
            adapter.InsertCommand = cmd;
            if (!(adapter.InsertCommand.ExecuteNonQuery() > 0))
            {
                conn.Close();
                return false;
            }
            return true;
        }


        public int RemoveCustomer( Guid Id)
        {
            Customer foundCustomer = FindCustomerById(Id);

            if (foundCustomer == null)
            {
                return 1;
            }
            string queryString = "DELETE [Prak].[dbo].[Customer] WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            adapter.DeleteCommand = cmd;
            if (!(adapter.DeleteCommand.ExecuteNonQuery() > 0))
            {
                conn.Close();
                return 2;
            }
            conn.Close();
            return 3;
        }


        public int ChangeAge( Customer newCustomerAge)
        {
            Customer foundCustomer = FindCustomerById(newCustomerAge.CustomerId);

            if (foundCustomer == null)
            {
                return 1;
            }
            string queryString = "UPDATE [Prak].[dbo].[Customer] SET Age = @newAge WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", newCustomerAge.CustomerId);
            cmd.Parameters.AddWithValue("@newAge", newCustomerAge.CustomerAge);
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            adapter.UpdateCommand = cmd;
            if (!(adapter.UpdateCommand.ExecuteNonQuery() > 0))
            {
                conn.Close();
                return 2;
            }
            conn.Close();
            return 3;
        }


    }

}

