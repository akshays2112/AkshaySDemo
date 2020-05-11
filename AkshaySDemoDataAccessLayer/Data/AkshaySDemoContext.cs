using AkshaySDemoDataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AkshaySDemoDataAccessLayer.Data
{
    internal class AkshaySDemoContext : DbContext
    {
        internal virtual DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SQLServer.ConnectionString);
            }
        }
    }
}
