﻿using BountyBoard.Core.Data;
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
    public class AccountManagementTests
    {

        public AccountManagement Resolve(Mock<IDatabaseContext> context, int? personId)
        {
            var accountGroup1 = new AccountGroup { Id = 1 };
            var person1 = new Person { Id = 1, };
            var person2 = new Person { Id = 2, DisabledDate = DateTime.Now };
            var sharedPerson = new Person { Id = 3, DisabledDate = DateTime.Now };

            var g1 = person1.AddToGroup(accountGroup1);
            var g2 = person2.AddToGroup(accountGroup1);
            var g3 = sharedPerson.AddToGroup(accountGroup1);

            var accountGroup2 = new AccountGroup { Id = 2 };
            var person4 = new Person { Id = 4 };
            var person5 = new Person { Id = 5 };

            var g4 = person1.AddToGroup(accountGroup2);
            var g5 = sharedPerson.AddToGroup(accountGroup2);
            var g6 = person4.AddToGroup(accountGroup2);
            var g7 = person5.AddToGroup(accountGroup2);

            var disabledAccountGroup = new AccountGroup() { Id = 3, EndDate = DateTime.Now };
            var person6 = new Person { Id = 6 };
            var g8 = person6.AddToGroup(disabledAccountGroup);
            var g9 = sharedPerson.AddToGroup(disabledAccountGroup);

            var accountGroup4 = new AccountGroup() { Id = 4 };
            var g10 = person6.AddToGroup(accountGroup4);

            var people = new[] { person1, person2, sharedPerson, person4, person5, person6 };

            context.Setup(x => x.List<Person>()).Returns(people.AsQueryable());
            context.Setup(x => x.List<AccountGroup>()).Returns(new AccountGroup[] { accountGroup1, accountGroup2, disabledAccountGroup, accountGroup4 }.AsQueryable());
            context.Setup(x => x.List<AccountGroupPeople>()).Returns(new AccountGroupPeople[] { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10 }.AsQueryable());
            return new AccountManagement(context.Object, personId.Value);
        }
        

        [TestMethod]
        public void GetMyColleagues_JustOnesBelongingToMyJoinedCompanies()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);
            var people = mangement.GetMyColleagues();

            Assert.IsFalse(people.Any(x => x.Id == 3));
            Assert.IsFalse(people.Any(x => x.Id == 2));
            Assert.IsTrue(people.Any(x => x.Id == 4));
            Assert.IsTrue(people.Any(x => x.Id == 5));
            Assert.IsFalse(people.Any(x => x.Id == 6));
        }

        [TestMethod]
        public void AddColleague_ExistingUser_CanYouNot()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);

            Person person = new Person { Id = 1 };
            var accountGroupId = 1;
            mangement.InvitePerson(person, accountGroupId);
            fakeContext.Verify(x => x.SaveChanges(), Times.Never);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void AddColleague_DisabledGroup_ThorwsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);

            mangement.InvitePerson(new Person { Id = 10 }, 3);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void AddColleague_NonExistingGroup_ThrowsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.InvitePerson(new Person { Id = 100 }, 323232);
        }

        [TestMethod]
        public void AddColleague_NewUser_OK()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);
            var newPersonId = 100;

            management.InvitePerson(new Person { Id = newPersonId }, 1);
            fakeContext.Verify(x => x.Add<AccountGroupPeople>(It.Is<AccountGroupPeople>(y => y.PersonId == newPersonId && y.AccountGroupId == 1)));
            fakeContext.Verify(x => x.SaveChanges());
        }

        [TestMethod]
        public void DisableMyAccount_CurrentOrganisation_IsPossible()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //person 1 only exists in org1 and org2
            var management = Resolve(fakeContext, 1);

            management.DisableMyAccount(4);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void DisableMyAccount_NotMyOrg_Wtfbro()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //person 1 only exists in org1 and org2
            var management = Resolve(fakeContext, 1); 

            management.DisableMyAccount(4);
        }
    }
}