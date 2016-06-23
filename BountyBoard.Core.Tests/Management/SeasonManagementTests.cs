using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class SeasonManagementTests
    {
        [TestMethod, TestCategory("Season Admin")]
        public void AddSeason_InitialAdd_AddsSeason()
        {
            //seasons can be added no problem
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Season Admin"), ExpectedException(typeof(BusinessLogicException))]
        public void ActivateSeason_InvalidStartDate_ThrowsException()
        {
            throw new NotImplementedException();
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Season Admin")]
        public void ActivateSeason_InvalidEndDate_ThrowsException()
        {
            throw new NotImplementedException();
        }
    }
}
