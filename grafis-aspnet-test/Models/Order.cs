using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace grafis_aspnet_test.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public virtual Client Client { get; set; }

        [Required]
        public double Value { get; set; }

        public double Discount { get; set; }

        [Required]
        public double TotalValue { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public virtual ICollection<OrderProduct> Products { get; set; }
    }
}