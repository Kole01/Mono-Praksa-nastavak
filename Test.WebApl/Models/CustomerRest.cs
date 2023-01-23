using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Test.Model.Common;

namespace Test.Repository
{
    public class CustomerRest
    {
        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public string CustomerLastName { get; set; }

        [Required]
        public int CustomerAge { get; set; }

    }

}