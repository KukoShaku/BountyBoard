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
    public class AutomatedControllerTests
    {
        [TestMethod, TestCategory("Automation")]
        public void ProcessSeasons_Mix_DoesEverything()
        {
            Mock<IDatabaseContext> context = new Mock<IDatabaseContext>();
            var controller = new AutomatedController(context.Object);
            //setup

            //act
            controller.ProcessSeasons();

            //expectations
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Automation")]
        public void ProcessInvitations_MixedUsers_SendsInvitations()
        {
            Mock<IDatabaseContext> context = new Mock<IDatabaseContext>();
            var controller = new AutomatedController(context.Object);
            //setup

            //act
            controller.ProcessInvitations();
        }

        
    }
}
