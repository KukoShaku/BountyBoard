using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Tests
{
    [TestClass]
    public class SeasonAchievementManagementTests
    {

        private SeasonAchievementManagement Resolve(IDatabaseContext context)
        {
            AchievementManagement a = new AchievementManagement(context);
            SeasonManagement s = new SeasonManagement(context);
            return new SeasonAchievementManagement(context, a, s);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Join_NonExistingAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);

            mangement.Join(-1, 1);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Join_NonExistingSeason_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Join(1, -1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void Join_ActiveSeason_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Join(1, 1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void Join_UnapprovedAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Join(1, 1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void Join_ExistingPair_ThrowsError()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Join(1, 1);
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Join_CorrectInput_AddsAndSaves()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Join(1, 1);
            throw new NotImplementedException();
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void Remove_ActiveSeason_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Remove(1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void Remove_NonExistingSeasonAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Remove(-1);
        }

        [TestMethod]
        public void Remove_CorrectRecord_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext.Object);
            mangement.Remove(1);

            throw new NotImplementedException();
        }
    }
}
