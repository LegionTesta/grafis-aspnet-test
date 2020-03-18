using grafis_aspnet_test.Context;
using grafis_aspnet_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using static grafis_aspnet_test.Controllers.ClientController;
using static grafis_aspnet_test.Controllers.ProductController;

namespace grafis_aspnet_test.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrderController : ApiController
    {

        public class OrderProductInfo
        {
            public ProductInfo product { get; set; }
            public double amount { get; set; }
        }

        public class OrderInfo
        {
            public long id { get; set; }
            public DateTime date { get; set; }
            public virtual ICollection<OrderProductInfo> products { get; set; }
            public ClientInfo client { get; set; }
            public double value { get; set; }
            public double discount { get; set; }
            public double totalValue { get; set; }
        }

        static OrderProductInfo getOrderProductData(OrderProduct orderProduct)
        {
            return new OrderProductInfo
            {
                product = ProductController.getInfo(orderProduct.Product),
                amount = orderProduct.Amount
            };
        }


        static OrderInfo getInfo(Order order)
        {
            return new OrderInfo
            {
                id = order.Id,
                date = order.Date,
                products = order.Products.Select(getOrderProductData).ToList(),
                client = ClientController.getInfo(order.Client),
                value = order.Value,
                discount = order.Discount,
                totalValue = order.TotalValue,
            };
        }

        public List<OrderInfo> Get()
        {
            using (var context = new DatabaseContext())
            {
                return context.Orders.Select(getInfo).ToList();
            }
        }

        public OrderInfo Get(int id)
        {
            using (var context = new DatabaseContext())
            {
                var orders = context.Orders.Find(id);
                return getInfo(orders);
            }
        }

        public class NewOrderProduct
        {
            public int ProductId { get; set; }
            public double Amount { get; set; }
        }

        public class NewOrder
        {
            public virtual List<NewOrderProduct> Products { get; set; }
            public long? ClientId { get; set; }
            public double Discount { get; set; }
        }



        public IHttpActionResult Post(NewOrder data)
        {
            using (var context = new DatabaseContext())
            {
                try
                {
                    var client = data.ClientId != null ? context.Clients.Find(data.ClientId) : null;
                    var products = new List<OrderProduct>();

                    if (data.Products != null)
                    {
                        foreach (var orderProductInfo in data.Products)
                        {
                            var p = context.Products.Find(orderProductInfo.ProductId);
                            if (p != null)
                            {
                                products.Add(new OrderProduct { Product = p, Amount = orderProductInfo.Amount, Date = DateTime.Now });
                            }
                            else
                            {
                                return BadRequest($"Produto {orderProductInfo.ProductId} não encontrado.");
                            }
                        }
                    }

                    double value = products?.Aggregate(0.0, (sum, next) => sum + (next.Amount * next.Product.Price)) ?? 0;
                    data.Discount = Math.Max(0, data.Discount);
                    double totalValue = Math.Max(value - data.Discount, 0);

                    var order = new Order()
                    {
                        Date = DateTime.Now,
                        Client = client,
                        Products = products,
                        Value = value,
                        Discount = data.Discount,
                        TotalValue = totalValue
                    };

                    order = context.Orders.Add(order);
                    context.SaveChanges();

                    return Ok<OrderInfo>(getInfo(order));
                }
                catch (DbEntityValidationException validationException)
                {
                    return BadRequest(validationException.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
                }
            }
        }
    }
}