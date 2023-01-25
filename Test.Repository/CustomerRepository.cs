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
using Test.Common;


namespace Test.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        public static string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
        

        public async Task<Customer> FindCustomerByIdAsync(Guid id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Customer foundCustomer = new Customer();
            string queryString = "SELECT [Id],[FirstName],[lastName],[Age] FROM [Prak].[dbo].[Customer] WHERE [Id]= @id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@id", id);
            await conn.OpenAsync();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (await reader.ReadAsync())
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

        public async Task<List<Customer>> AllCustomersAsync(Paging paging, Sorting sorting, Filtering filtering)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            List<Customer> customers = new List<Customer>();
            StringBuilder queryString = new StringBuilder("SELECT * FROM [Prak].[dbo].[Customer]");

            if (filtering.CustomerAgeMin!= null && filtering.CustomerAgeMax != null )
            {
                queryString.AppendLine("WHERE Age > " + filtering.CustomerAgeMin + " AND Age < " + filtering.CustomerAgeMax);
            }

            if (filtering.CustomerAgeMin == null && filtering.CustomerAgeMax != null)
            {
                queryString.AppendLine("WHERE Age < " + filtering.CustomerAgeMax);
            }

            if (filtering.CustomerAgeMin != null && filtering.CustomerAgeMax == null)
            {
                queryString.AppendLine("WHERE Age > " + filtering.CustomerAgeMin);
            }

            if (filtering.SearchCustomer!=null && (filtering.CustomerAgeMax!=null || filtering.CustomerAgeMin!=null ))
            {
                queryString.AppendLine(" AND LastName LIKE '%"+ filtering.SearchCustomer+ "%' OR FirstName LIKE '%"+ filtering.SearchCustomer+"%'");
            }
            if (filtering.SearchCustomer != null && (filtering.CustomerAgeMax == null && filtering.CustomerAgeMin == null))
            {
                queryString.AppendLine(" WHERE LastName LIKE '%"+filtering.SearchCustomer+ "%' OR FirstName LIKE '%"+ filtering.SearchCustomer+"%'");
            }
            queryString.AppendLine("ORDER BY "+ sorting.OrderBy + " " + sorting.OrderDirection);
            queryString.AppendLine("OFFSET " + (paging.PageNumber - 1) * paging.Rpp + " ROWS");
            queryString.AppendLine("FETCH NEXT "+ paging.Rpp + " ROWS ONLY");


            SqlCommand cmd = new SqlCommand(queryString.ToString(), conn);
            await conn.OpenAsync();
            SqlDataReader reader = cmd.ExecuteReader();
            while (await reader.ReadAsync())
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

        public async Task<bool> SaveCustomerAsync(Customer newCustomer)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            newCustomer.CustomerId = Guid.NewGuid();
            string queryString = "INSERT INTO Customer (Id,FirstName,LastName, Age) VALUES (@id,@CustomerFirstName,@CustomerLastName,@CustomerAge)";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            await conn.OpenAsync();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@id", newCustomer.CustomerId);
            cmd.Parameters.AddWithValue("@CustomerFirstName", newCustomer.CustomerFirstName);
            cmd.Parameters.AddWithValue("@CustomerLastName", newCustomer.CustomerLastName);
            cmd.Parameters.AddWithValue("@CustomerAge", newCustomer.CustomerAge);
            adapter.InsertCommand = cmd;
            if (!(await adapter.InsertCommand.ExecuteNonQueryAsync() > 0))
            {
                conn.Close();
                return false;
            }
            return true;
        }


        public async Task<int> RemoveCustomerAsync( Guid Id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Customer foundCustomer = await FindCustomerByIdAsync(Id);

            if (foundCustomer == null)
            {
                return 1;
            }
            string queryString = "DELETE [Prak].[dbo].[Customer] WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            await conn.OpenAsync();
            adapter.DeleteCommand = cmd;
            if (!(await adapter.DeleteCommand.ExecuteNonQueryAsync() > 0))
            {
                conn.Close();
                return 2;
            }
            conn.Close();
            return 3;
        }


        public async Task<int> UpdateCustomerAsync( Customer updateCustomer)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Customer foundCustomer = await FindCustomerByIdAsync(updateCustomer.CustomerId);
            
            if (foundCustomer == null)
            {
                return 1;
            }
            
            if (String.IsNullOrEmpty(updateCustomer.CustomerFirstName))
            {
                updateCustomer.CustomerFirstName = foundCustomer.CustomerFirstName;
            }
            if (String.IsNullOrEmpty(updateCustomer.CustomerLastName))
            {
                updateCustomer.CustomerLastName = foundCustomer.CustomerLastName;
            }
            if (updateCustomer.CustomerAge== 0)
            {
                updateCustomer.CustomerAge = foundCustomer.CustomerAge;
            }

            string queryString = "UPDATE [Prak].[dbo].[Customer] SET Age = @newAge, FirstName = @NewFirstName, LastName = @newLastName WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", updateCustomer.CustomerId);
            cmd.Parameters.AddWithValue("@newFirstName", updateCustomer.CustomerFirstName);
            cmd.Parameters.AddWithValue("@newLastName", updateCustomer.CustomerLastName);
            cmd.Parameters.AddWithValue("@newAge", updateCustomer.CustomerAge);
            SqlDataAdapter adapter = new SqlDataAdapter();
            try
            {
                await conn.OpenAsync();
                adapter.UpdateCommand = cmd;
                if (!(await adapter.UpdateCommand.ExecuteNonQueryAsync() > 0))
                {
                    conn.Close();
                    return 2;
                }
                conn.Close();
                return 3;
            }catch (SqlException ex)
            {
                throw ex;
                
            }
            
        }


    }

}

