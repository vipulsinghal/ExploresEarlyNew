namespace ExplorersEarlyLearning.Migrations
{
    using ExplorersEarlyLearning.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ExplorersEarlyLearning.DAL.ExploresEarlyLearningContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ExplorersEarlyLearning.DAL.ExploresEarlyLearningContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //var menus = new List<Menu>
            //{
            //    new Menu { Name = "Home", PageTitle = "Home", OrderIndex=1,IsActive=true,PageType=PageType.Home},
            //    new Menu { Name = "Philosophy", PageTitle = "Philosophy", OrderIndex=2,IsActive=true,PageType=PageType.Philosophy},
            //    new Menu { Name = "Programs", PageTitle = "Program", OrderIndex=3,IsActive=true,PageType=PageType.Program},
            //    new Menu { Name = "Centers", PageTitle = "Center", OrderIndex=4,IsActive=true,PageType=PageType.Center},
            //    new Menu { Name = "Portfolios", PageTitle = "Portfolio", OrderIndex=5,IsActive=true,PageType=PageType.Portfolio},
            //    new Menu { Name = "Employment", PageTitle = "Employment", OrderIndex=6,IsActive=true,PageType=PageType.Employment},
            //    new Menu { Name = "Contact Us", PageTitle = "ContactUs", OrderIndex=7,IsActive=true,PageType=PageType.ContactUs},
            //};
            //menus.ForEach(s => context.Menus.AddOrUpdate(p => p.Name, s));
            //context.SaveChanges();
        }
    }
}
