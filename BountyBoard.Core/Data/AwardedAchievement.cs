using System;

namespace BountyBoard.Core.Data
{
    public class AwardedAchievement : DatabaseObject
    {

        internal AwardedAchievement() { }

        public SeasonAchievement SeasonAchievement { get; set; }
        public int SeasonAchievementId { get; set; }
        public DateTime AwardDate { get; set; }
    }
}