using AkshaySDemoModels;
using Microsoft.EntityFrameworkCore;

namespace AkshaySDemoMySQL.Data
{
    public class AkshaySDemoMySQLContext : DbContext
    {
        public static string ConnectionString { get; set; }
        internal virtual DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(ConnectionString);
            }
        }
    }
}

/*
MySQL akshaysdemodb
-------------------
CREATE TABLE Recipes(
	ID int NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Name nvarchar(256) NULL,
	Description nvarchar(512) NULL,
	Comment nvarchar(1024) NULL,
	CreatorsName nvarchar(256) NULL,
	Notes nvarchar(5000) NULL
)
*/ 
