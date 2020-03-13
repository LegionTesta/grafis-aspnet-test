using grafis_aspnet_test.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace grafis_aspnet_test.Context
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DatabaseContext() : base("DatabaseString")
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