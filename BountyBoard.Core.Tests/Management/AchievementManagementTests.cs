using BountyBoard.Core.Data;
using BountyBoard.Core.Management;
using BountyBoard.Core.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BountyBoard.Core.Test.Extensions;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class AchievementManagementTests
    {
        [TestMethod, TestCategory("Achievement"), ExpectedException(typeof(BusinessLogicException))]
        public void GiveAchievement_BadPersonDetails_Fails()
        {
            //give an achievement with incorrect person details.
            var fakeContext = new Mock<IDatabaseContext>();
            Guid key = Guid.NewGuid();
            var achievements = new[] 
            {
                new Achievement
                {
                    Key = key,
                }
            };
            fakeContext.Setup(x => x.List<Achievement>())
                .Returns(achievements.AsQueryable());

            //this is the only person in the database
            var people = new[] { new Person { Id = 100 } };
            fakeContext.Setup(x => x.List<Person>())
                .Returns(people.AsQueryable());

            AchievementManagement mng = new AchievementManagement(fakeContext.Object);
            var allocation = new AchievementAllocation();
            allocation.AchievementKey = key;
            allocation.IsManual = true;
            //this is the wrong person
            allocation.CustomPersonKey = "1";

            mng.GiveAchievement(allocation);
        }

        [TestMethod, TestCategory("Achievement"), ExpectedException(typeof(BusinessLogicException))]
        public void GiveAchievement_BadAccountGroupDetails_Fails()
        {
            //give an achievement to the incorrect account group
            var fakeContext = new Mock<IDatabaseContext>();
            var season = new Season
            {
                Id = 10000,
            };
            Guid key = Guid.NewGuid();
            var achievements = new[]
            {
                new Achievement
                {
                    Key = key,
                    Season = season,
                    SeasonId = season.Id
                }
            };
            fakeContext.Setup(x => x.List<Achievement>())
                .Returns(achievements.AsQueryable());

            //this is the only person in the database
            var person = new Person { Id = 100 };
            var accountGroup = new AccountGroup() {  };
            person.AddToGroup(accountGroup, 12);
            var people = new[] { person };
            fakeContext.Setup(x => x.List<Person>())
                .Returns(people.AsQueryable());

            season.AccountGroup = accountGroup;

            AchievementManagement mng = new AchievementManagement(fakeContext.Object);
            var allocation = new AchievementAllocation();

            allocation.CustomPersonKey = person.Id.ToString();
            allocation.AchievementKey = key;

            mng.GiveAchievement(allocation);
        }

        [TestMethod, TestCategory("Achievement"), ExpectedException(typeof(InvalidOperationException))]
        public void GiveAchievement_NonExistingAchievement_Fails()
        {
            var fakeContext = new Mock<IDatabaseContext>();
            var mng = new AchievementManagement(fakeContext.Object);

            var allocation = new AchievementAllocation
            {
                AchievementKey = Guid.NewGuid(),
            };
            mng.GiveAchievement(allocation);
        }

        [TestMethod, TestCategory("Achievement"), ExpectedException(typeof(BusinessLogicException))]
        public void GiveAchievement_InactiveAchievement_Fails()
        {
            //give an achievement that is currently inactive
            var key = Guid.NewGuid();
            var fakeContext = new Mock<IDatabaseContext>();
            var mng = new AchievementManagement(fakeContext.Object);
            fakeContext.Setup(x => x.List<Achievement>()).Returns(
                new[]
                {
                    new Achievement()
                    {
                        Key = key,
                        Season = new Season
                        {
                            EndDate = DateTime.Now.AddDays(2),
                            IsActive = true,
                            StartDate = DateTime.Now,
                        }
                    }
                }.AsQueryable());

            var allocation = new AchievementAllocation
            {
                AchievementKey = key,
                
            };

            mng.GiveAchievement(allocation);
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_NoActiveSeasons_Fails()
        {
            var key = Guid.NewGuid();
            var fakeContext = new Mock<IDatabaseContext>();
            var mng = new AchievementManagement(fakeContext.Object);

            var season1 = new Season
            {
                StartDate = DateTime.Now.AddDays(-100),
                EndDate = DateTime.Now.AddDays(-88),                
                IsActive = true,
                Achievements = new [] 
                {
                    new Achievement
                    {
                        Key = key,
                        IsApproved = true,
                    }
                }
            };
            var season2 = new Season
            {
                StartDate = DateTime.Now.AddDays(-87),
                EndDate = DateTime.Now.AddDays(-70),
                IsActive = true,
                Achievements = new [] 
                {
                    new Achievement
                    {
                        Key = key,
                        IsApproved = true, 
                    }
                }
            };

            fakeContext.Setup(x => x.List<Season>()).Returns(
                new[]
                {
                    season1, season2
                }.AsQueryable());

            var allocation = new AchievementAllocation()
            {
                AchievementKey = key,
            };
            mng.GiveAchievement(allocation);
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_MultiSeasonWithOneActive_Success()
        {
            //this needs to provide at least two seasons 
            //that both have two different acheivements with the same key
            //this will only work if one of the seasons is the current applicable
            //season
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_CorrectDetails_AddsToDatabase()
        {
            //give an achievement with the right key, with season being active
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Audit")]
        public void GiveAchievement_CorrectDetails_HasAuditInformation()
        {
            //this needs to track whether or not it's made by api or not
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_CorrectDetails_AddsToPersonWallet()
        {
            //give an achievement adds value to a person's wallet
            throw new NotImplementedException();
        }
        

    }
}
