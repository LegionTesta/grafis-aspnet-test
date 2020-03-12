using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace grafis_aspnet_test.Models
{
    public class Order
    {

        [Required]
        public Client Client { get; set; }

        [Required]
        public double Value { get; set; }

        public double Discount { get; set; }

        [Required]
        public double TotalValue { get; set; }
    }
}