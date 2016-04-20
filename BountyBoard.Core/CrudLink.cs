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

        public IEnumerable<T> List { get; }
        public void AddOrUpdate(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

    }
}
