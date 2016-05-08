using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    /// <summary>
    /// can't think of a beter name for it
    /// The purpose of this is for all metric related queries to end up here.
    /// EG. how many achievements have i done.
    /// All statistical shit needs to go in here
    /// </summary>
    public class MetricsHub : UserRestrictedDatabaseLink
    {

        public MetricsHub(IDatabaseContext context, int personId)
            : base(context, personId)
        {

        }


    }
}
