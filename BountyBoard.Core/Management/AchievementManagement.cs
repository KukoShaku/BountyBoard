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
            Season activeSeason = key.AccountGroup.CurrentActiveSeason;
            Achievement relevantAchievement = activeSeason.Achievements.SingleOrDefault(x => x.Key == allocation.AchievementKey);
            if (relevantAchievement == null)
            {
                throw new BusinessLogicException("There is no relevant achievement with the created date specified");
            }

            AccountGroupPerson targetPersonJoin = key.AccountGroup.AccountGroupPeople.SingleOrDefault(x => x.CustomKey == allocation.CustomPersonKey);
            if (targetPersonJoin == null)
            {
                throw new UnauthorizedAccessException("Person does not exist, have you setup their customKey correctly?");
            }

            GiveAchievement(targetPersonJoin, relevantAchievement, allocation.CreatedDate);
        }

        private void GiveAchievement(AccountGroupPerson accountGroupPerson, Achievement achievement, DateTime awardedOn)
        {
            PersonWallet wallet = accountGroupPerson.Person.Wallets.Single(x => x.AccountGroupId == accountGroupPerson.AccountGroupId);
            wallet.Value += achievement.Value;
            AwardedAchievement award = new AwardedAchievement()
            {
                Achievement = achievement,
                AchievementId = achievement.Id,
                AwardDate = awardedOn,
            };

            Context.Add(award);
            Context.SaveChanges();
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
