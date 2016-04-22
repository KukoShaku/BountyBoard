using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public abstract class UserRestrictedDatabaseLink : DatabaseLink
    {
        private readonly Person _person;
        protected Person Person { get { return _person; } }
        public bool ValidUser { get { return !_person.DisabledDate.HasValue; } }

        protected UserRestrictedDatabaseLink(IDatabaseContext context, int personId) 
            : base(context)
        {
            _person = Context.List<Person>().Single(x => x.Id == personId && !x.DisabledDate.HasValue);
        }
    }
}
