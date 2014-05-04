using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Explorers.Infrastructure.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}