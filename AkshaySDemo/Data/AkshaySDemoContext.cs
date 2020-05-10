using AkshaySDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AkshaySDemo.Data
{
    public class AkshaySDemoContext : DbContext
    {
        public virtual DbSet<Recipe> Recipes { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.ConnStr);
            }
        }
    }
}
