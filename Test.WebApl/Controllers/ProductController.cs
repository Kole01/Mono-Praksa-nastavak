using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Test.WebApl.Models;

namespace Test.WebApl.Controllers
{
    public class ProductController : ApiController
    {
        public static List<Product> products = new List<Product>
        {
            //new Product(1, "Jabuka", 10.5),
            //new Product(2, "Kruska", 9),
            //new Product(3, "Sljiva", 8),
            new Product(4, "Banana", 7),
            new Product(5, "Limun", 5),
            new Product(6, "Mandarina", 2)
        };



        [HttpGet]
        // GET: api/Values
        public HttpResponseMessage findProductById(int id)
        {
            var foundProduct = products.Find(product => product.ProductId == id);
            if (foundProduct != null) return Request.CreateResponse(HttpStatusCode.OK, foundProduct);
            else return Request.CreateResponse(HttpStatusCode.NotFound, "No such product!");
        }


        [HttpGet]
        // GET: api/Values/5
        public HttpResponseMessage AllProducts()
        {
            if (products!=null)
                return Request.CreateResponse(HttpStatusCode.OK, products);
            else return Request.CreateResponse(HttpStatusCode.NotFound,"Empty!");
        }


        [HttpPost]
        // POST: api/Values
        public HttpResponseMessage saveProduct([FromBody] Product newProduct)
        {

            if (!products.Exists(product=> product.ProductId==newProduct.ProductId))
            {
                products.Add(newProduct);
                return Request.CreateResponse(HttpStatusCode.OK, "Added!");
            }
            else return Request.CreateResponse(HttpStatusCode.BadRequest,"Porduct with the same id exists!");
        }


            
        [HttpPut]
        // PUT: api/Values/5
        public HttpResponseMessage changePrice([FromUri]int id, [FromUri]double value)
        {
            var foundProduct = products.Find(Product => Product.ProductId == id);
            foundProduct.ProductPrice = value;
            if (foundProduct != null)
            {
                if (foundProduct.ProductPrice == value) return Request.CreateResponse(HttpStatusCode.OK, "Changed!");
                else return Request.CreateResponse(HttpStatusCode.NotModified, "Error!");
            }
            else return Request.CreateResponse(HttpStatusCode.NotFound, "Not found!");
        }

        [HttpDelete]
        // DELETE: api/Values/5
        public HttpResponseMessage removeProduct([FromUri]int id)
        {   
            int numberOfProducts = products.Count;
            products.Remove(products.Find(product => product.ProductId == id));
            if (products.Count== numberOfProducts-1) return Request.CreateResponse(HttpStatusCode.OK,"Deleted!");
            else return Request.CreateResponse(HttpStatusCode.NotModified,"Error!"); ;
        }
    }
}
