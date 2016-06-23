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
            //obvious check
            if (allocation == null)
            {
                throw new ArgumentNullException(nameof(allocation));
            }
            //do some not so obvious things to self diagnose
            allocation.Validate();

            //validate this key
            var key = Context.List<ApiKey>().Single(x => x.Key == allocation.ApiKey);
            if (!key.IsActive)
            {
                throw new UnauthorizedAccessException("Api Key has been disabled, please contact your administrator");
            }

            //get the achievement that matches the key
            IEnumerable<Achievement> achievements = key.AccountGroup.Achievements
                .Where(x => x.Key == allocation.AchievementKey);
            //this gets the only active achievement that can be applied
            Achievement relevantAchievement = achievements.SingleOrDefault(x => x.Season.IsActive
                && x.Season.IsBetween(allocation.CreatedDate));
            if (relevantAchievement == null)
            {
                throw new BusinessLogicException("There is no relevant achievement with the created date specified");
            }

            AccountGroupPeople targetPersonJoin = key.AccountGroup.AccountGroupPeople.SingleOrDefault(x => x.CustomKey == allocation.CustomPersonKey);
            if (targetPersonJoin == null)
            {
                throw new UnauthorizedAccessException("Person does not exist, have you setup their customKey correctly?");
            }


            throw new NotImplementedException();
        }

        private void EnsureAchievementIsValid(Person person, Guid key)
        {
            var seasons = person.AccountGroupPeople.Select(x => x.AccountGroup).SelectMany(x => x.Seasons);
            if (seasons == null || !seasons.Any())
            {

            }
        }
        
    }
}
