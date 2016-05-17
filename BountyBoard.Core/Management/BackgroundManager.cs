using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class BackgroundManager : DatabaseLink
    {
        public BackgroundManager(IDatabaseContext context) 
            : base(context)
        {
            //maybe do some form of authentication??
        }

        public void ProcessSeasons()
        {
            throw new NotImplementedException();
        }

        public void InvalidateAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
