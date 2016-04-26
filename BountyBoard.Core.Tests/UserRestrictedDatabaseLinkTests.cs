using BountyBoard.Core.Data;
using BountyBoard.Core.Test.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class UserRestrictedDatabaseLinkTests
    {
        [TestMethod]
        public void Constructor_FakeRestrictiveAccess_ActiveUser()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { new Person { Id = 1 } }.AsQueryable());
            var access = new FakeRestrictiveAccess(fakeContext.Object, 1);

            Assert.IsTrue(access.ValidUser);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void Constructor_DisabledAccount_ThrowsException()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] 
            {
                new Person
                {
                    Id = 1,
                    DisabledDate = DateTime.Now
                }
            }.AsQueryable());
            var access = new FakeRestrictiveAccess(fakeContext.Object, 1);
        }
    }
}
