using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class SeasonAchievementManagement : DatabaseLink
    {
        private readonly AchievementManagement _achievementManagement;
        private readonly SeasonManagement _seasonManagement;

        //takes two crud items
        public SeasonAchievementManagement(IDatabaseContext context, AchievementManagement achievementManagement, SeasonManagement seasonManagement)
            : base(context)
        {
            _achievementManagement = achievementManagement;
            _seasonManagement = seasonManagement;
        }

        /// <summary>
        /// Joins achievement to season if they exist and season is not active and achievement is approved
        /// </summary>
        /// <param name="achievementId"></param>
        /// <param name="seasonId"></param>
        public void Join(int achievementId, int seasonId)
        {
            //these two need to both exist
            var achievement = _achievementManagement.List.Single(x => x.Id == achievementId);
            var season = _seasonManagement.List.Single(x => x.Id == seasonId);

            if (season.IsActive)
            {
                throw new BusinessLogicException("Dude, you can't just add new achievements after they've been activated");
            }

            if (!achievement.IsApproved)
            {
                throw new BusinessLogicException("Achievement must be approved");
            }

            var result = new SeasonAchievement() {
                Achievement = achievement,
                Season = season,
            };

            throw new NotImplementedException("Implement the insertion bit");
        }

        public void Remove(int id)
        {
            throw new NotImplementedException("Implement the deletion bit");
        }
    }
}
