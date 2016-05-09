using BountyBoard.Core.Data;
using BountyBoard.Core.Test.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BountyBoard.Core.Test.Extensions;

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class UserRestrictedDatabaseLinkTests
    {
        [TestMethod, TestCategory("Security")]
        public void Constructor_FakeRestrictiveAccess_ActiveUser()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { new Person { Id = 1 } }.AsQueryable());
            var access = new FakeRestrictiveAccess(fakeContext.Object, 1);

            Assert.IsTrue(access.ValidUser);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Security")]
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

        [TestMethod]
        public void MyPermissions_MegaList_Works()
        {
            //person1, is disabled
            //person2 is the target source
            //person3 is just an average user across different companies
            //what is happening!!?!!??!!?!???
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var person1 = new Person { Id = 1 };
            var person2 = new Person { Id = 2 };
            var person3 = new Person { Id = 3 };

            var group1 = new AccountGroup { Id = 1 };
            var group2 = new AccountGroup { Id = 2 };
            var group3 = new AccountGroup { Id = 3, EndDate = DateTime.Today.AddDays(-1) };

            person1.AddToGroup(group1, 1);

            //person 2 is an admin user in group 2
            var p2g1 = person2.AddToGroup(group1, 2);
            p2g1.PermissionLevel = PermissionLevel.Admin;
            var p2g2 = person2.AddToGroup(group2, 3);
            p2g2.PermissionLevel = PermissionLevel.SuperAdmin;

            person3.AddToGroup(group1, 4);
            person3.AddToGroup(group2, 5);
            person3.AddToGroup(group3, 6);

            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { person1, person2, person3 }.AsQueryable());

            //not sure what this is suppose to test
            var access1 = new FakeRestrictiveAccess(fakeContext.Object, 1);
            Assert.AreEqual(1, access1.MyPermissions.Count());

            //needs to return exactly an admin record and a superadmin record
            var access2 = new FakeRestrictiveAccess(fakeContext.Object, 2);
            Assert.IsTrue(access1.MyPermissions.Any(x => x.Value == PermissionLevel.Admin && x.Key.Id == 1));
            Assert.IsTrue(access1.MyPermissions.Any(x => x.Value == PermissionLevel.SuperAdmin && x.Key.Id == 2));
            Assert.IsFalse(access1.MyPermissions.Any(x => x.Value == PermissionLevel.Normal));
            Assert.IsFalse(access1.MyPermissions.Any(x => x.Value == PermissionLevel.Disable));


            //this is to test what's happening with disabled account groups
            var access3 = new FakeRestrictiveAccess(fakeContext.Object, 3);
            Assert.AreEqual(2, access3.MyPermissions.Count());
        }
    }
}
