using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class SeasonManagement : CrudLink<Season>
    {
        public SeasonManagement(IDatabaseContext context) : base(context)
        {

        }
    }
}
