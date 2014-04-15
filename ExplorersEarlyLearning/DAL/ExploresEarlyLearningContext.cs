using ExplorersEarlyLearning.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ExplorersEarlyLearning.DAL
{
    
        public class ExploresEarlyLearningContext : DbContext
        {
            public DbSet<Menu> Menus { get; set; }
            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            }
        
    }
}