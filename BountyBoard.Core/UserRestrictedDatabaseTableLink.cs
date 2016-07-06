using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public abstract class UserRestrictedDatabaseTableLink<T> : UserRestrictedDatabaseLink where T: DatabaseObject
    {
        protected UserRestrictedDatabaseTableLink(IDatabaseContext context, int personId)
            : base(context, personId)
        {

        }
    }
}
