using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class Templates
    {

        [TestMethod, TestCategory("Usability")]
        public void ZZTop5Achievements_Person()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Usability")]
        public void ZZTrackProgres_person()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Pets")]
        public void ZZPetStatus_NoPets()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Usability")]
        public void ZZAlmostThereAchievements()
        {

            //everyone wants to see what is within their reach
            throw new NotImplementedException();

        }

        [TestMethod, TestCategory("Usability")]
        public void ZZRollTodaysAchievements()
        {
            throw new NotImplementedException("Pick an achievement for the day? Or week? and have it save in the database");

        }

        [TestMethod, TestCategory("Usability")]
        public void ZZHallOfFame_Person()
        {
            throw new NotImplementedException("I would like to see all of my previous highlights"); 
        }

        [TestMethod, TestCategory("Usability")]
        public void ZZAdditiveAchievements()
        {
            throw new NotImplementedException("Deal with additive acheivements that are based on events");
        }
    }
}
