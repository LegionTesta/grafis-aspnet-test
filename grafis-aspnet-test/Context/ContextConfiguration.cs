using System.Data.Entity.Migrations;

namespace grafis_aspnet_test.Context
{
    public class ContextConfiguration : DbMigrationsConfiguration<DatabaseContext>
    {
        public ContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "AspNetWebApi";
        }
    }
}