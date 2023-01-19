using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Test.WebApl.Models
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public String CustomerFirstName { get; set; }
        public String CustomerLastName { get; set; }
        public int CustomerAge { get; set; }



    }
}