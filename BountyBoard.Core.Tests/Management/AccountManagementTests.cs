using BountyBoard.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BountyBoard.Core.Test.Extensions;
using BountyBoard.Core.ViewModels;
using BountyBoard.Core.Management;

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class AccountManagementTests
    {
        /// <summary>
        /// A template for a whole bunch of things.
        /// Account groups 1, 2
        /// People 1-5 join to account groups in various ways. Some users
        /// have been disabled
        /// </summary>
        /// <param name="currentPerson"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public AccountManagement Resolve(Mock<IDatabaseContext> context, int currentPerson)
        {
            var accountGroup1 = new AccountGroup { Id = 1 };
            var person1 = new Person { Id = 1, };
            var person2 = new Person { Id = 2, DisabledDate = DateTime.Now };
            var sharedPerson = new Person { Id = 3, DisabledDate = DateTime.Now };

            var g1 = person1.AddToGroup(accountGroup1, 1);
            var g2 = person2.AddToGroup(accountGroup1, 2);
            var g3 = sharedPerson.AddToGroup(accountGroup1, 3);

            var accountGroup2 = new AccountGroup { Id = 2 };
            var person4 = new Person { Id = 4 };
            var person5 = new Person { Id = 5 };

            var g4 = person1.AddToGroup(accountGroup2, 4);
            var g5 = sharedPerson.AddToGroup(accountGroup2, 5);
            var g6 = person4.AddToGroup(accountGroup2, 6);
            var g7 = person5.AddToGroup(accountGroup2, 7);

            var disabledAccountGroup = new AccountGroup() { Id = 3, EndDate = DateTime.Now };
            var person6 = new Person { Id = 6 };
            var g8 = person6.AddToGroup(disabledAccountGroup, 8);
            var g9 = sharedPerson.AddToGroup(disabledAccountGroup, 9);

            var accountGroup4 = new AccountGroup() { Id = 4 };
            var g10 = person6.AddToGroup(accountGroup4, 10);

            var people = new[] { person1, person2, sharedPerson, person4, person5, person6 };

            context.Setup(x => x.List<Person>()).Returns(people.AsQueryable());
            context.Setup(x => x.List<AccountGroup>()).Returns(new AccountGroup[] { accountGroup1, accountGroup2, disabledAccountGroup, accountGroup4 }.AsQueryable());
            context.Setup(x => x.List<AccountGroupPeople>()).Returns(new AccountGroupPeople[] { g1, g2, g3, g4, g5, g6, g7, g8, g9, g10 }.AsQueryable());
            return new AccountManagement(context.Object, currentPerson);
        }


        [TestMethod, TestCategory("Usability")]
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

        [TestMethod, TestCategory("Usability")]
        public void GetMyColleagues_ForCompany_JustThatCompany()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);
            var people = mangement.GetMyColleagues(2);


            Assert.IsFalse(people.Any(x => x.Id == 2));
            Assert.IsFalse(people.Any(x => x.Id == 3));
            Assert.IsTrue(people.Any(x => x.Id == 4));
            Assert.IsTrue(people.Any(x => x.Id == 5));
            Assert.IsFalse(people.Any(x => x.Id == 6));
        }

        [TestMethod, TestCategory("Usability")]
        public void GetColleagues_IncludeDisabledPeople_Works()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);
            var people = mangement.GetMyColleagues(true);

            Assert.IsTrue(people.Any(x => x.Id == 2));
            Assert.IsTrue(people.Any(x => x.Id == 3));
            Assert.IsTrue(people.Any(x => x.Id == 4));
            Assert.IsTrue(people.Any(x => x.Id == 5));
            Assert.IsFalse(people.Any(x => x.Id == 6));
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Admin")]
        public void InvitePerson_DisabledGroup_ThrowsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);

            mangement.InvitePerson(new PersonInvitation() {
                AccountGroupId = 3,
                Email = "test",
                Name = "dun matter"
            });
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Admin")]
        public void AddColleague_NonExistingGroup_ThrowsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.InvitePerson(new PersonInvitation()
            {
                AccountGroupId = 999, //this account group id doesn't exist in resolve()
                Email = "Testemail@something.com",
                Name = "TestUser",
            });
            //this expects that the get account groups doesn't exist
        }

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_NewUser_OK()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.InvitePerson(new PersonInvitation()
            {
                Email = "Yes",
                Name = "yes",
                AccountGroupId = 1,
            });

            fakeContext.Verify(x => x.Add(It.Is<Invitation>(y => y.Name == "yes")), Times.Once);
            fakeContext.Verify(x => x.SaveChanges());
        }

        [TestMethod, TestCategory("Admin")]
        public void DisableMyAccount_CurrentOrganisation_IsPossible()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //person 1 only exists in org1 and org2
            var management = Resolve(fakeContext, 1);

            management.DisableMyAccount(2);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Security")]
        public void DisableMyAccount_NotMyOrg_Wtfbro()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            //person 1 only exists in org1 and org2
            var management = Resolve(fakeContext, 1);

            management.DisableMyAccount(4);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Security")]
        public void DisableAccount_NotMyOrg_WtfBro()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.DisableAccount(6, 4);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Security")]
        public void DisableAccount_NotMyColleague_WtfBro()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.DisableAccount(6, 2);
        }

        [TestMethod, TestCategory("Admin")]
        public void DisableAccount_MyOrg_SavesChanges()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.DisableAccount(3, 2);

            fakeContext.Setup(x => x.SaveChanges());
            fakeContext.Verify(x => x.Delete<AccountGroupPeople>(5), Times.Once);
        }

        private AccountManagement SimpleResolve(Mock<IDatabaseContext> fakeContext, int v, params int[] accountGroupIds)
        {
            var me = new Person
            {
                Id = v,
            };

            var accountGroups = accountGroupIds.Select(x => new AccountGroup
            {
                Id = x,
            });

            me.AccountGroupPeople = accountGroups.Select(x => new AccountGroupPeople() { AccountGroup = x, AccountGroupId = x.Id }).ToList();

            fakeContext.Setup(x => x.List<AccountGroupPeople>()).Returns(me.AccountGroupPeople.AsQueryable());
            fakeContext.Setup(x => x.List<AccountGroup>()).Returns(accountGroups.AsQueryable());
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { me }.AsQueryable());
            return new AccountManagement(fakeContext.Object, v);
        }

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_DifferentAccountGroup_AddsInvitation()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            AccountManagement management = SimpleResolve(fakeContext, 1, 1, 2);

            AccountGroup accountGroup = new AccountGroup { Id = 6, };
            Person someoneElse = new Person
            {
                Id = 2,
            };

            string email = "hello world";
            var invitation = new PersonInvitation
            {
                Email = email,
                AccountGroupId = 2,
                Name = "dun matter"
            };

            management.InvitePerson(invitation);
            fakeContext.Verify(x => x.Add(It.IsAny<Invitation>()), Times.Once);
        }

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_AlreadyInvitedBySomeoneElse_UpdatesExpirationDate()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            AccountManagement management = SimpleResolve(fakeContext, 1, 1);
            //this needs to be 1 because you can only add groups that you belong to
            AccountGroup accountGroup = new AccountGroup { Id = 1, }; 
            Person someoneElse = new Person
            {
                Id = 2,
            };

            string email = "test";
            var subjectInvitation = new Invitation
            {
                EmailAddress = email,
                AccountGroup = accountGroup,
                AccountGroupId = accountGroup.Id,
                InvitedBy = someoneElse,
                InvitedById = someoneElse.Id,
                ExpirationDate = DateTime.MinValue,
            };

            fakeContext.Setup(x => x.List<Invitation>()).Returns(new[] { subjectInvitation }.AsQueryable());
            management.InvitePerson(new PersonInvitation()
            {
                Email = email,
                AccountGroupId = accountGroup.Id,
                Name = "test"
            });

            //we will test to see that database update is called as well as expiration date has been reset
            fakeContext.Verify(x => x.SaveChanges(), Times.Once);
            Assert.IsTrue(subjectInvitation.ExpirationDate > DateTime.Now);
        }
    }
}
