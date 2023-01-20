using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using Test.Model.Common;

namespace Test.Model
{
    public class Product : IProduct
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
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