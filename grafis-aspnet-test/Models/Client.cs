using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace grafis_aspnet_test.Models
{
    public class Client
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(40)]
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}