using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Explorers.Infrastructure.Entities;

namespace DataAccess
{
        public class ExDatabaseContext : DbContext
        {
            public DbSet<Menu> Menus { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<WebPage> WebPages { get; set; }

            public ExDatabaseContext():base("ExplorersEarlyLearningContext"){}

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();


                modelBuilder.Entity<WebPage>()
            .HasMany(x => x.ChildPages)
            .WithOptional(x => x.Parent)
            .HasForeignKey(x=>x.ParentId);
            }
        
    }
}