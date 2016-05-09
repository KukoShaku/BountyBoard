using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class ReceivedEventsManager : DatabaseLink
    {
        public ReceivedEventsManager(IDatabaseContext context)
            : base(context)
        {

        }
    }
}
