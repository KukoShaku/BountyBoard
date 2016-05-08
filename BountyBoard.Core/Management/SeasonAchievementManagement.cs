using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class SeasonAchievementManagement : UserRestrictedDatabaseLink
    {
        private readonly AchievementManagement _achievementManagement;
        private readonly SeasonManagement _seasonManagement;

        //takes two crud items
        public SeasonAchievementManagement(IDatabaseContext context, int personId, AchievementManagement achievementManagement, SeasonManagement seasonManagement)
            : base(context, personId)
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

            var seasonAchievement = Context.List<SeasonAchievement>()
                .SingleOrDefault(x => x.SeasonId == season.Id && x.AchievementId == achievement.Id);
            if (seasonAchievement != null)
            {
                throw new BusinessLogicException("Achievement already exists in this season");
            }

            var result = new SeasonAchievement
            {
                AchievementId = achievement.Id,
                Achievement = achievement,
                Season = season,
                SeasonId = season.Id
            };

            Context.Add(result);
            Context.SaveChanges();
        }

        public void Remove(int id)
        {
            var seasonAchievement = Context.List<SeasonAchievement>().Single(x => x.Id == id);
            if (seasonAchievement.Season.IsActive)
            {
                throw new BusinessLogicException("Season is currently active, you can't remove an active season.");
            }
            else
            {
                Context.Delete<SeasonAchievement>(id);
                Context.SaveChanges();
            }
        }
    }
}
