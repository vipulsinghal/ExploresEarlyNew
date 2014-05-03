using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class RepoBase:IDisposable
    {
        public ExDatabaseContext DbContext;
        public RepoBase(ExDatabaseContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public void Save()
        {
            DbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);

        }
    }
}
