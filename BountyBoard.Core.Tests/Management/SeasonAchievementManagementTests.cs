﻿using BountyBoard.Core.Data;
using BountyBoard.Core.Management;
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

        private SeasonAchievementManagement Resolve(Mock<IDatabaseContext> context, bool isValidUser)
        {
            if (isValidUser)
            {
                context.Setup(x => x.List<Person>()).Returns(new[] { new Person { Id = 1 } }.AsQueryable());
            }
            AchievementManagement a = new AchievementManagement(context.Object);
            SeasonManagement s = new SeasonManagement(context.Object, 1);
            return new SeasonAchievementManagement(context.Object, 0, a, s);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Achievement Admin")]
        public void Join_NonExistingAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1 } }.AsQueryable());
            fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            mangement.Join(-1, 1);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Achievement Admin")]
        public void Join_NonExistingSeason_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1 } }.AsQueryable());
            fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            mangement.Join(1, -1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Achievement Admin")]
        public void Join_ActiveSeason_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1, IsApproved = true } }.AsQueryable());
            fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1, IsActive = true } }.AsQueryable());
            mangement.Join(1, 1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Achievement Admin")]
        public void Join_UnapprovedAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1, IsApproved = false } }.AsQueryable());
            fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            mangement.Join(1, 1);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Achievement Admin")]
        public void Join_ExistingPair_ThrowsError()
        {
            //join a season that already has this achievement key
            throw new NotImplementedException();
            //Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            //fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1, IsApproved = true } }.AsQueryable());
            //fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            //fakeContext.Setup(x => x.List<SeasonAchievement>()).Returns(new SeasonAchievement[] { new SeasonAchievement { SeasonId = 1, AchievementId = 1 } }.AsQueryable());
            //mangement.Join(1, 1);
        }

        [TestMethod, TestCategory("Achievement Admin")]
        public void Join_CorrectInput_AddsAndSaves()
        {
            //when joining an achievement to season, remember to add it as a new
            //achievement with the same key.
            throw new NotImplementedException();
            //Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            //fakeContext.Setup(x => x.List<Achievement>()).Returns(new Achievement[] { new Achievement { Id = 1, IsApproved = true } }.AsQueryable());
            //fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            //mangement.Join(1, 1);

            //fakeContext.Verify(x => x.Add<SeasonAchievement>(It.IsAny<SeasonAchievement>()), Times.Once);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Achievement Admin")]
        public void Remove_ActiveSeason_Fails()
        {
            //attempt removing an active season should fail
            throw new NotImplementedException();
            //Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            //fakeContext.Setup(x => x.List<SeasonAchievement>()).Returns(new SeasonAchievement[] { new SeasonAchievement { Id = 1, Season = new Season { Id = 1, IsActive = true } } }.AsQueryable());
            //mangement.Remove(1);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Achievement Admin")]
        public void Remove_NonExistingSeasonAchievement_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            fakeContext.Setup(x => x.List<Season>()).Returns(new Season[] { new Season { Id = 1 } }.AsQueryable());
            mangement.Remove(-1);
        }

        [TestMethod, TestCategory("Achievement Admin")]
        public void Remove_CorrectRecord_CallsDelete()
        {
            //when removing an achievement from the season, make sure that the
            //season is not active and the achievement exists in the season
            throw new NotImplementedException();
            //Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //SeasonAchievementManagement mangement = Resolve(fakeContext, true);
            //fakeContext.Setup(x => x.List<SeasonAchievement>()).Returns(new SeasonAchievement[] { new SeasonAchievement { Id = 1, Season = new Season { IsActive = false} } }.AsQueryable());
            //mangement.Remove(1);

            //fakeContext.Verify(x => x.Delete<SeasonAchievement>(1), Times.Once);
            
        }

    }
}
