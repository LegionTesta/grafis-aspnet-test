using System.ComponentModel.DataAnnotations;

namespace grafis_aspnet_test.Models
{
    public class OrderProduct 
    {
        [Required]
        public long Id { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        [Required]
        public virtual Product Product { get; set; }

        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double Amount { get; set; }
    }
}