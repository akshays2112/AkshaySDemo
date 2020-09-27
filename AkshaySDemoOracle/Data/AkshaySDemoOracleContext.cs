using AkshaySDemoModels.Oracle;
using Microsoft.EntityFrameworkCore;

/*
Oracle 19c akshaysdemodb
------------------------
CREATE TABLE Recipes (
  ID NUMBER GENERATED ALWAYS AS IDENTITY,
  Name NVARCHAR2(2000) NOT NULL,
  Description NVARCHAR2(2000) NOT NULL,
  "Comment" NVARCHAR2(2000) NOT NULL,
  CreatorsName NVARCHAR2(2000) NOT NULL,
  Notes NVARCHAR2(2000) NOT NULL
);

create user dbo identified by "P@ssword123";

ALTER SESSION SET CURRENT_SCHEMA = SYS;

GRANT CONNECT TO dbo;
GRANT DBA TO dbo;

*/

namespace AkshaySDemoOracle.Data
{
    public partial class AkshaySDemoOracleContext : DbContext
    {
        public static string ConnectionString { get; set; }
        
        internal virtual DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle(ConnectionString);
            }
        }
    }
}
