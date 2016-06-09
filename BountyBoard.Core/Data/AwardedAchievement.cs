using System;

namespace BountyBoard.Core.Data
{
    public class AwardedAchievement : DatabaseObject
    {

        internal AwardedAchievement() { }

        public Achievement Achievement { get; set; }
        public int AchievementId { get; set; }
        public DateTime AwardDate { get; set; }
    }
}