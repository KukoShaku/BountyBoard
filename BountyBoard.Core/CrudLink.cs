using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class CrudLink<T> : DatabaseLink where T : DatabaseObject
    {
        protected CrudLink(IDatabaseContext context) 
            : base(context) { }

        public IEnumerable<T> List { get { return Context.List<T>(); } }
        internal void AddOrUpdate(T item)
        {
            if (item.Id >= 0)
            {
                Context.Update(item);
            }
            else
            {
                Context.Add(item);
            }

            Context.SaveChanges();
        }

        internal void Delete(int id)
        {
            Context.Delete<T>(id);
            Context.SaveChanges();
        }

    }
}
