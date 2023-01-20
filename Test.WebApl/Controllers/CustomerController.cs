using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebSockets;
using Test.Model;
using Test.Repository;
using Test.Service;
using Test.Service.Common;
namespace Test.WebApl.Controllers
{
    public class CustomerController : ApiController
    {


        CustomerService service = new CustomerService();

        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage FindCustomerById(Guid id)
        {
            Customer foundCustomer = service.FindCustomerById(id);
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
            List<Customer> customers = service.AllCustomers();
            List<CustomerRest> listRestCustomers = new List<CustomerRest>();
            foreach (Customer customer in customers)
            {
                CustomerRest RestCustomer = new CustomerRest()
                {
                    CustomerFirstName = customer.CustomerFirstName,
                    CustomerLastName = customer.CustomerLastName,
                    CustomerAge=customer.CustomerAge,

                };
                listRestCustomers.Add(RestCustomer);
            }
            if (listRestCustomers != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, listRestCustomers);
            }
               
            return Request.CreateResponse(HttpStatusCode.NotFound, "Empty!");
        }


        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage SaveCustomer([FromBody]CustomerRest customerToSave)
        {
            Customer newCustomer = new Customer(Guid.NewGuid(),customerToSave.CustomerFirstName, customerToSave.CustomerLastName, customerToSave.CustomerAge);
            if (service.SaveCustomer(newCustomer))
            {
                return Request.CreateResponse(HttpStatusCode.OK,customerToSave);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,"Greska");

        }



        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage ChangeAge([FromBody] CustomerRest updateCustomerRest)
        {
     
            switch (service.ChangeAge(updateCustomer))
            {
                case 1:
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");

                case 2:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
                    
                case 3:
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                default:
                    return null;
            }
            
            
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage RemoveCustomer([FromUri] Guid Id)
        {

            switch (service.RemoveCustomer(Id))
            {
                case 1:
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");

                case 2:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");

                case 3:
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                default:
                    return null;
            }
        }
                       
       
    }
}
