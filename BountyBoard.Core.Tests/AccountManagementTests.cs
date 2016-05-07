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

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class AccountManagementTests
    {

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

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_ExistingUserNewGroup_CanYouNot()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);

            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { new Person { } }.AsQueryable());

            Person person = new Person { Id = 1 };
            var accountGroupId = 1;
            mangement.InvitePerson(new PersonInvitation() { Email = "email", AccountGroupId = accountGroupId, });
            fakeContext.Verify(x => x.SaveChanges(), Times.Never);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Admin")]
        public void InvitePerson_DisabledGroup_ThrowsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mangement = Resolve(fakeContext, 1);

            mangement.InvitePerson(new PersonInvitation());
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Admin")]
        public void AddColleague_NonExistingGroup_ThrowsExceptions()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);

            management.InvitePerson(new PersonInvitation());
        }

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_NewUser_OK()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = Resolve(fakeContext, 1);
            var newPersonId = 100;

            management.InvitePerson(new PersonInvitation());
            fakeContext.Verify(x => x.Add<AccountGroupPeople>(It.Is<AccountGroupPeople>(y => y.PersonId == newPersonId && y.AccountGroupId == 1)));
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

        [TestMethod, TestCategory("Admin")]
        public void CreateAccount_BadUser_AddsPerson()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var management = SimpleResolve(fakeContext, 1);

            throw new NotImplementedException();
        }

        private AccountManagement SimpleResolve(Mock<IDatabaseContext> fakeContext, int v)
        {
            var me = new Person { Id = v };
            
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { me }.AsQueryable());
            return new AccountManagement(fakeContext.Object, v);   
        }
        

        [TestMethod, TestCategory("Admin")]
        public void InvitePerson_AlreadyInvited_NothingHappens()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            AccountManagement management = SimpleResolve(fakeContext, 1);

            management.InvitePerson(new PersonInvitation());

        }
    }
}
