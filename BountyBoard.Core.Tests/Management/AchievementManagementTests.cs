using BountyBoard.Core.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class AchievementManagementTests
    {
        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_BadPersonDetails_Fails()
        {
            var fakeContext = new Mock<IDatabaseContext>();
            AchievementManagement mng = new AchievementManagement(fakeContext.Object);
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_BadAccountGroupDetails_Fails()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_NonExistingAchievement_Fails()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_InactiveAchievement_Fails()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_IncorrectSeason_Fails()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Achievement")]
        public void GiveAchievement_CorrectDetails_AddsToDatabase()
        {
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
            throw new NotImplementedException();
        }
        

    }
}
