using System.Data.Entity.Migrations;

namespace grafis_aspnet_test.Context
{
    public class ContextConfiguration : DbMigrationsConfiguration<Context>
    {
        public ContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "AspNetWebApi";
        }
    }
}