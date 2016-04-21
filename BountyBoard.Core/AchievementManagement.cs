using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class AchievementManagement: CrudLink<Achievement>
    {
        public AchievementManagement(IDatabaseContext context) : base(context)
        {

        }
    }
}
