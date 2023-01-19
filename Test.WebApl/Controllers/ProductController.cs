using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.WebApl.Models;
using System.Data.SqlClient;
using System.Web.ModelBinding;
using System.Data;
using Microsoft.Win32.SafeHandles;

namespace Test.WebApl.Controllers
{
    public class ProductController : ApiController
    {

        public static string connectionString = "Data Source=st-03\\SQLEXPRESS;Initial Catalog=Prak;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connectionString);



        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage FindProductById(Guid Id)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            Product foundProduct = FindingProduct(Id);

            if (foundProduct != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, foundProduct);
            }

            return Request.CreateResponse(HttpStatusCode.NotFound, "No such product!");

        }


        [HttpGet]
        // GET: api/Values/5
        public HttpResponseMessage AllProducts()
        {
            
            List<Product> products = new List<Product>();
            string queryString = "SELECT [Id],[ProductName],[ProductPrice] FROM [Prak].[dbo].[Product]";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Product tempProduct = new Product();
                tempProduct.ProductName = reader.GetString(1);
                tempProduct.ProductId = reader.GetGuid(0);
                tempProduct.ProductPrice = reader.GetDouble(2);
                products.Add(tempProduct);
            }
            conn.Close();
            if (products != null)
                return Request.CreateResponse(HttpStatusCode.OK, products);

            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty!");
        }


         [HttpPost]
// POST: api/Values
        public HttpResponseMessage SaveProduct([FromBody] Product newProduct)
        {
            
            newProduct.ProductId = Guid.NewGuid();
            string queryString = "INSERT INTO Product (Id,ProductName,ProductPrice) VALUES (@id,@productName,@productPrice)";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            conn.Open();
            SqlDataAdapter adapter = new SqlDataAdapter();
            cmd.Parameters.AddWithValue("@id", newProduct.ProductId);
            cmd.Parameters.AddWithValue("@productName", newProduct.ProductName);
            cmd.Parameters.AddWithValue("@productPrice", newProduct.ProductPrice);
            adapter.InsertCommand = cmd;
            adapter.InsertCommand.ExecuteNonQuery();
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK);
           
        }



        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage ChangePrice([FromBody] Product NewProductPrice)
        {
            
            Product foundProduct = FindingProduct(NewProductPrice.ProductId);
            if(foundProduct==null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No product for update");
            }
          
            string queryString = "UPDATE [Prak].[dbo].[Product] SET ProductPrice = @value WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", NewProductPrice.ProductId);
            cmd.Parameters.AddWithValue("@value", NewProductPrice.ProductPrice);
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            adapter.UpdateCommand= cmd;
            if (!(adapter.UpdateCommand.ExecuteNonQuery() > 0))
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
            }

            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");


        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveProduct([FromUri] Guid Id)
        {
            Product foundProduct = FindingProduct(Id);

            if (foundProduct == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "No Product found");
            }
           
            string queryString = "DELETE [Prak].[dbo].[Product] WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@Id", Id);
            SqlDataAdapter adapter = new SqlDataAdapter();
            conn.Open();
            adapter.DeleteCommand = cmd;
            if (!(adapter.DeleteCommand.ExecuteNonQuery()> 0))
            {
                conn.Close();
                return Request.CreateResponse(HttpStatusCode.OK, "Failed");
            }
            conn.Close();
            return Request.CreateResponse(HttpStatusCode.OK, "Success");
        }

        public Product FindingProduct(Guid Id)
        {
            Product foundProduct = new Product();
            string queryString = "SELECT [Id],[ProductName],[ProductPrice] FROM [Prak].[dbo].[Product] WHERE [Id]= @id";
            SqlCommand cmd = new SqlCommand(queryString, conn);
            cmd.Parameters.AddWithValue("@id", Id);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    foundProduct.ProductName = reader.GetString(1);
                    foundProduct.ProductId = reader.GetGuid(0);
                    foundProduct.ProductPrice = reader.GetDouble(2);

                }
                conn.Close();
            }

            if (foundProduct == null)
            {
                return null;
            }
            return foundProduct;
        }

    }
}
    

