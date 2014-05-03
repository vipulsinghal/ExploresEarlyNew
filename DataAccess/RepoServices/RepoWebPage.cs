using Explorers.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace DataAccess.RepoServices
{
    public class RepoWebPage:RepoBase
    {
        public RepoWebPage(ExDatabaseContext dbContext):base(dbContext){ }

        public IList<WebPage> GetAllPages() {
            var lstPages = DbContext.WebPages.Where(x=>x.Parent == null);
            return lstPages.Any() ? lstPages.Include(x=>x.ChildPages).OrderBy(x => x.DisplaySequence).ToList() : new List<WebPage>();
        }
        public IList<WebPage> GetAllPagesTest(out string errMsg)
        {
            errMsg = string.Empty;
            var lstPages = new List<WebPage>
            {
                new WebPage() {Id = 1, Title = "Home", DisplaySequence = 0, IsPublished = true, Type = WebPageType.Home},
                new WebPage() {Id = 2, Title = "Philosophy", DisplaySequence = 1, IsPublished = true, Type = WebPageType.Philosophy},
                new WebPage()
                {
                    Id = 3, Title = "Programs", DisplaySequence = 2, IsPublished = true, Type = WebPageType.Program, ChildPages = 
                        new List<WebPage>
                        {
                new WebPage() {Id = 31, Title = "Emergent Curriculum", DisplaySequence = 0, IsPublished = true, Type = WebPageType.Program},
                new WebPage() {Id = 32, Title = "Early Years Framework", DisplaySequence = 1, IsPublished = true, Type = WebPageType.Program}
                            
                        }
                },
                new WebPage()
                {
                    Id = 4, Title = "Centres", DisplaySequence = 3, IsPublished = true, Type = WebPageType.Centre, ChildPages = 
                         new List<WebPage>
                        {
                new WebPage() {Id = 41, Title = "Abbotsford / Richmond", DisplaySequence = 0, IsPublished = true, Type = WebPageType.Centre},
                new WebPage() {Id = 42, Title = "Maidstone / Maribyrnong", DisplaySequence = 1, IsPublished = true, Type = WebPageType.Centre}
                            
                        }
                },
                new WebPage() {Id = 5, Title = "Portfolios", DisplaySequence = 4, IsPublished = true, Type = WebPageType.Portfolio},
                new WebPage() {Id = 6, Title = "Employment", DisplaySequence = 5, IsPublished = true, Type = WebPageType.Employment},
                new WebPage() {Id = 7, Title = "Contact Us", DisplaySequence = 6, IsPublished = true, Type = WebPageType.ContactUs}
            };
            return lstPages;
        }
    }
}
