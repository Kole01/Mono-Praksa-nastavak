using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.WebSockets;
using Test.Model;
using Test.Model.Common;
using Test.Repository;
using Test.Repository.Common;
using Test.Service;
using Test.Service.Common;
using Test.WebApl.Models;
namespace Test.WebApl.Controllers
{
    public class CustomerController : ApiController
    {

        public CustomerController() { }
        //CustomerService service = new CustomerService();

        private ICustomerService service { get; set; }

        public CustomerController(ICustomerService _service)
        {
            service = _service;
        }

        [HttpGet]
        // GET: api/Values
        public async Task<HttpResponseMessage> FindCustomerByIdAsync(Guid id)
        {
            Customer foundCustomer = await service.FindCustomerByIdAsync(id);
            if (foundCustomer != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, foundCustomer);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No such Customer!");

        }




        [HttpGet]
        // GET: api/Values/5
        public async Task<HttpResponseMessage> AllCustomersAsync()
        {
            List<Customer> customers = await service.AllCustomersAsync();
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
        public async Task<HttpResponseMessage> SaveCustomerAsync([FromBody]CustomerRest customerToSave)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad User");
            }
            Customer newCustomer = new Customer(Guid.NewGuid(),customerToSave.CustomerFirstName, customerToSave.CustomerLastName, customerToSave.CustomerAge);
            if (await service.SaveCustomerAsync(newCustomer))
            {
                return Request.CreateResponse(HttpStatusCode.OK,customerToSave);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest,"Greska");

        }



        [HttpPut]
        // PUT: api/Values/5
        public async Task<HttpResponseMessage> UpdateCustomerAsync([FromBody] UpdateCustomerRest updateCustomer)
        {
            if(String.IsNullOrEmpty(updateCustomer.CustomerId.ToString()))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Greska");
            }


            Customer alterCustomer = new Customer()
            {
                CustomerId = updateCustomer.CustomerId,
                CustomerFirstName = updateCustomer.CustomerFirstName,
                CustomerLastName = updateCustomer.CustomerLastName,
                CustomerAge = updateCustomer.CustomerAge
            };

            switch (await service.UpdateCustomerAsync(alterCustomer))
            {
                case 1:
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");

                case 2:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
                    
                case 3:
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
            }
            
            
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public async Task<HttpResponseMessage> RemoveCustomerAsync([FromUri] Guid Id)
        {

            switch (await service.RemoveCustomerAsync(Id))
            {
                case 1:
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Customer not found");

                case 2:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");

                case 3:
                    return Request.CreateResponse(HttpStatusCode.OK, "Success");

                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Failed");
            }
        }
                       
       
    }
}
