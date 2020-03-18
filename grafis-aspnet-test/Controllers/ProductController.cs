using grafis_aspnet_test.Context;
using grafis_aspnet_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace grafis_aspnet_test.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        public class ProductInfo
        {
            public long id { get; set; }
            public string desc { get; set; }
            public double price { get; set; }
        }

        public static ProductInfo getInfo(Product product)
        {
            return new ProductInfo
            {
                id = product.Id,
                desc = product.Desc,
                price = product.Price,
            };
        }

        public List<ProductInfo> Get()
        {
            using (var context = new DatabaseContext())
            {
                return context.Products.Select(getInfo).ToList();
            }
        }

        public ProductInfo Get(int id)
        {
            using (var context = new DatabaseContext())
            {
                var products = context.Products.Find(id);
                return getInfo(products);
            }
        }

        public class NewProduct
        {
            public string Desc { get; set; }
            public double Price { get; set; }
        }

        public IHttpActionResult Post(NewProduct data)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    data.Desc = data.Desc?.Trim();

                    var product = new Product()
                    {
                        Desc = data.Desc,
                        Price = data.Price,
                    };

                    product = context.Products.Add(product);
                    context.SaveChanges();

                    return Ok<ProductInfo>(getInfo(product));
                }
                catch (DbEntityValidationException validationException)
                {
                    return BadRequest(validationException.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                }

            }
        }
    }
}