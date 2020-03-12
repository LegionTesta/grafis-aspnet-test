using grafis_aspnet_test.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace grafis_aspnet_test.Context
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public Context() : base("ConnectionString")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Client>()
            .HasIndex(c => c.Email)
            .IsUnique();
        }

    }
}