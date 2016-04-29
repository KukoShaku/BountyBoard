using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.MockObjects
{
    public class FakeRestrictiveAccess : UserRestrictedDatabaseLink
    {
        public FakeRestrictiveAccess(IDatabaseContext context, int personId) 
            : base(context, personId)
        {

        }

        
    }
}
