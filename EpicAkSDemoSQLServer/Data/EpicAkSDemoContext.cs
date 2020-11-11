using EpicAksSModels;
using Microsoft.EntityFrameworkCore;

namespace EpicAkSDemoSQLServer.Data
{
    public class EpicAkSDemoContext : DbContext
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
