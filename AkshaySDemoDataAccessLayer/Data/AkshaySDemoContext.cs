using AkshaySDemoModels;
using Microsoft.EntityFrameworkCore;

namespace AkshaySDemoSQLServer.Data
{
    public class AkshaySDemoContext : DbContext
    {
        public static string ConnectionString { get; set; }
        internal virtual DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
