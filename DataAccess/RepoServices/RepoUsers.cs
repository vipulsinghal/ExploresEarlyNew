using Explorers.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.RepoServices
{
    public class RepoUsers : RepoBase
    {
        public RepoUsers(ExDatabaseContext dbContext) : base(dbContext) { }

        public IList<User> GetAllUsers()
        {
            return DbContext.Users.Where(x => x.IsActive==true).ToList();
        }
    }
}
