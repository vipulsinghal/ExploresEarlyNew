using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace Explorers.Infrastructure.Entities
{
    public class WebPage
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public WebPageType Type { get; set; }

        public IList<WebPage> ChildPages { get; set; }

        public int? ParentId{ get; set; }
        public WebPage Parent { get; set; }
        public int DisplaySequence { get; set; }
        public bool IsPublished { get; set; }

    }
}
