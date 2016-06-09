using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class AutomatedController : DatabaseLink
    {
        public AutomatedController(IDatabaseContext context)
            :base(context)
        {

        }

        public void ProcessInvitations()
        {
            throw new NotImplementedException();
        }

        internal void ProcessSeasons()
        {
            throw new NotImplementedException();
        }
    }
}
