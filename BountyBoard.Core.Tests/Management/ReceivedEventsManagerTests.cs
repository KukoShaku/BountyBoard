using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class ReceivedEventsManagerTests
    {
        [TestMethod]
        public void AddData_SomeData_AddsToDatabase()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void AddData_VerifiedKey_AddsToDatabase()
        {
            throw new NotImplementedException("Key is the perscribed key for the account group.");
        }

        [TestMethod]
        public void Constructor_WrongKey_OK()
        {
            throw new NotImplementedException("Wrong security key for th");
        }

        [TestMethod]
        public void Process_WithFailures_SavedChangesWithErrors()
        {
            throw new NotImplementedException();
        }

        [TestMethod, TestCategory("Automation")]
        public void Processes_NoFailures_SavedChangesWithNoErrors()
        {
            throw new NotImplementedException();
        }
    }
}
