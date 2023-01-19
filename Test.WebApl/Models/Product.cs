using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace Test.WebApl.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public String ProductName { get; set; }
        public double ProductPrice { get; set; }
        public Product() { }

        public Product(Guid productId, string productName, double productPrice)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
        }       
    }
}