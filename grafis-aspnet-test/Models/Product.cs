using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace grafis_aspnet_test.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Desc { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]

        public double Price { get; set; }

        public virtual ICollection<OrderProduct> Order { get; set; }
    }
}