using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ExplorersEarlyLearning.HtmlHelpers
{
    public class MenuItem
    {
        public MenuItem() {
        }
        
        public MenuItem(string name, string url, int index) {
            this.Name = name;
            this.Url = url;
            this.Index = index;
        }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Index { get; set; }
    }
    public class TabMenuHelper
    {
        public static IList<MenuItem> GetMenuItems()
        {
            List<MenuItem> menuItems = new List<MenuItem>() { 
                      new MenuItem("Home","/Home",1),new MenuItem("Philosophy","/Philosophy",2),
                      new MenuItem("Programs","/Programs",3),new MenuItem("Centres","#",4),
                      new MenuItem("Portfolios","/Portfolios",5),new MenuItem("Employment","/Employment",6),
                      new MenuItem("Contact us","/Contactus",7)
                };
            return menuItems;
        }

    }
    public static class NavigationHelper
    {
        public static MvcHtmlString NavMenu(int selectedIndex, int subselectedIndex)
        {
            var ul = new TagBuilder("ul");
            //var url = new UrlHelper(helper.ViewContext.RequestContext);
            
            foreach (var item in TabMenuHelper.GetMenuItems())
            {
                var li = new TagBuilder("li");
                var anchor = new TagBuilder("a");
                
                    anchor.MergeAttribute("href", item.Url);
                
                anchor.InnerHtml = item.Name;
                if (selectedIndex == item.Index) anchor.AddCssClass("active");
                
                li.InnerHtml += anchor.ToString();
                //li.InnerHtml += "<ul><li><a href=''>Test1</a></li><li><a href=''>Test2</a></li></ul>";

                  if(item.Name=="Programs"){

                      li.InnerHtml += "<ul class='biggerWidth'><li><a href='/Programs/AnEmergent' " + (subselectedIndex == 1 ? "class='active'" : "") + ">Emergent Curriculum</a></li>" +
                            "<li><a href='/Programs/EarlyYearsFramework' " + (subselectedIndex == 2 ? "class='active'" : "") + ">Early Years Framework</a></li>" +
                            "<li><a href='/Programs/EnvironmentalFocus' " + (subselectedIndex == 3 ? "class='active'" : "") + ">Environmental Program</a></li>" +
                            "<li><a href='/Programs/KindergartenProgram' " + (subselectedIndex == 4 ? "class='active'" : "") + ">Kindergarten Program</a></li>" +
                            "<li><a href='/Programs/ReadyProgram' " + (subselectedIndex == 5 ? "class='active'" : "") + ">Ready Program</a></li></ul>";
                    }

                  if (item.Name == "Centres")
                  {
                      li.InnerHtml += "<ul><li><a href='/Centres/AbbotsfordRichmond' " + (subselectedIndex == 6 ? "class='active'" : "") + ">Abbotsford / Richmond</a></li>" +
                                "<li><a href='/Centres/MaidstoneMaribyrnong' " + (subselectedIndex == 7 ? "class='active'" : "") + ">Maidstone / Maribyrnong</a></li></ul>";
                  }

                ul.InnerHtml += li.ToString();
            }
            return MvcHtmlString.Create(ul.ToString());
        }
    }
}