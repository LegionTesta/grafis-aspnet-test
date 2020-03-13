using System.ComponentModel.DataAnnotations;

namespace grafis_aspnet_test.Models
{
    public class Order
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public Client Client { get; set; }

        [Required]
        public double Value { get; set; }

        public double Discount { get; set; }

        [Required]
        public double TotalValue { get; set; }
    }
}