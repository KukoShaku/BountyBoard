using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class SeasonAchievement : DatabaseObject
    {
        internal SeasonAchievement() { }

        public int SeasonId { get; set; }
        public Season Season { get; set; }
        public int AchievementId { get; set; }
        public Achievement Achievement { get; set; }


    }
}
