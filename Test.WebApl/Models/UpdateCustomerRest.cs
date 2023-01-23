using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.WebApl.Models
{
    public class UpdateCustomerRest
    {
        [Required]
        public Guid CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public int CustomerAge { get; set; }

        public UpdateCustomerRest() { }

        public UpdateCustomerRest(Guid customerId, string customerFirstName, string customerLastName, int customerAge)
        {
            this.CustomerId = customerId;
            this.CustomerFirstName = customerFirstName;
            this.CustomerLastName = customerLastName;
            this.CustomerAge = customerAge;
        }
    }
}