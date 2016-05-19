using BountyBoard.Core.Data;
using BountyBoard.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class AchievementManagement: CrudLink<Achievement>
    {
        public AchievementManagement(IDatabaseContext context) 
            : base(context)
        {

        }

        public void GiveAchievement(AchievementAllocation allocation)
        {
            throw new NotImplementedException();
        }
        
    }
}
