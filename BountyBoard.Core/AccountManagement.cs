using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class AccountManagement : UserRestrictedDatabaseLink
    {
        public AccountManagement(IDatabaseContext context, int personId)  
            : base (context, personId)
        {

        }

        public void GetMyColleagues()
        {
            
        }
    }
}
