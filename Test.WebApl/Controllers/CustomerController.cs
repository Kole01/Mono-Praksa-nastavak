using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.WebApl.Models;

namespace Test.WebApl.Controllers
{
    public class CustomerController : ApiController
    {

        public static string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);

        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage FindCustomerById(Guid id)
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

            if (foundCustomer != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, foundCustomer);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No such Customer!");

        }


        [HttpGet]
        // GET: api/Values/5
        public HttpResponseMessage AllCustomers()
        {
            string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
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
                tempCustomer.CustomerAge = reader.GetInt32(3);
                customers.Add(tempCustomer);
            }
            conn.Close();
            if (customers != null)
                return Request.CreateResponse(HttpStatusCode.OK, customers);
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Empty!");
        }


        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage SaveCustomer([FromBody]Customer newCustomer)
        {
            string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            Guid id = Guid.NewGuid();
            string queryString = "INSERT INTO Customer (Id,FirstName,LastName, Age) VALUES (@id,@CustomerFirstName,@CustomerLastName,@CustomerAge)";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@CustomerFirstName", newCustomer.CustomerFirstName);
            cmd.Parameters.AddWithValue("@CustomerLastName", newCustomer.CustomerLastName);
            cmd.Parameters.AddWithValue("@CustomerAge", newCustomer.CustomerAge);
            adapter.InsertCommand = cmd;
            adapter.InsertCommand.ExecuteNonQuery();
            conn.Close();



            return Request.CreateResponse(HttpStatusCode.OK);

        }



        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage ChangeAge([FromUri] Guid Id, [FromUri] string newAge)
        {
            Customer foundCustomer = FindingCustomer(Id);

            if (foundCustomer == null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Failed");
            }
            string queryString = "UPDATE [Prak].[dbo].[Customer] SET Age = @newAge WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            cmd.Parameters.AddWithValue("@newAge", newAge);
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            adapter.UpdateCommand = cmd;
            if (!(adapter.UpdateCommand.ExecuteNonQuery() > 0))
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Failed"); 
            }
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveCustomer([FromUri] Guid Id)
        {
            Customer foundCustomer = FindingCustomer(Id);
            
            if (foundCustomer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");
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
                return Request.CreateResponse(HttpStatusCode.OK, "Failed");
            }
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }
                       
        public Customer FindingCustomer(Guid Id)
        {
            Customer foundCustomer = new Customer();
            string queryString = "SELECT [Id],[FirstName],[lastName],[Age] FROM [Prak].[dbo].[Customer] WHERE [Id]= @id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@id", Id);
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
    }
}
