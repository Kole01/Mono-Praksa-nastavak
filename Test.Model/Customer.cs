using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Test.Model.Common;

namespace Test.Model
{
    public class Customer : ICustomer
    {
        public Guid CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int CustomerAge { get; set; }

        public Customer() { }

        public Customer(Guid customerId, string customerFirstName, string customerLastName, int customerAge)
        {
            this.CustomerId = customerId;
            this.CustomerFirstName = customerFirstName;
            this.CustomerLastName = customerLastName;   
            this.CustomerAge = customerAge; 
        }


    }
}