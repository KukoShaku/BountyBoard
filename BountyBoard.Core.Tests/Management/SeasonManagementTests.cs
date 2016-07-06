using BountyBoard.Core.Data;
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
            var fakeContext = new Mock<IDatabaseContext>();
            Person person = new Person()
            {
                Id = 100
            };

            AccountGroupPerson personJoin = new AccountGroupPerson()
            {
                Person = person,
                PersonId = person.Id,
                PermissionLevel = PermissionLevel.Admin
            };
            AccountGroup accountGroup = new AccountGroup()
            {
                Id = 1000,
                AccountGroupPeople = new List<AccountGroupPerson>()
                {
                    personJoin,
                }
            };
            personJoin.AccountGroup = accountGroup;
            personJoin.AccountGroupId = accountGroup.Id;
            person.AccountGroupPeople = new[] { personJoin };
            Season season = new Season
            {
                Id = 2,
                IsActive = false,
                AccountGroup = accountGroup,
                StartDate = null,
                EndDate = DateTime.Today,
                AccountGroupId = accountGroup.Id,
            };

            fakeContext.Setup(x => x.List<Season>()).Returns(new[] { season }.AsQueryable());
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { person }.AsQueryable());
            SeasonManagement management = new SeasonManagement(fakeContext.Object, 100);
            management.ActivateSeason(2);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Season Admin")]
        public void ActivateSeason_InvalidEndDate_ThrowsException()
        {
            var fakeContext = new Mock<IDatabaseContext>();
            Person person = new Person
            {
                Id = 1,
            };
            var accountGroupPeople = new List<AccountGroupPerson>()
            {
                new AccountGroupPerson
                {
                    PermissionLevel = PermissionLevel.Admin, // this is imporant
                    PersonId = 1,
                    Person = person,
                }
            };
            person.AccountGroupPeople = accountGroupPeople;
            Season season = new Season
            {
                Id = 1000,
                IsActive = false, //important
                EndDate = DateTime.Today.AddDays(1),
                AccountGroup = new AccountGroup()
                {
                    AccountGroupPeople = accountGroupPeople
                },
            };
            
            fakeContext.Setup(x => x.List<Season>()).Returns(new[] { season }.AsQueryable());
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { person }.AsQueryable());
            var mng = new SeasonManagement(fakeContext.Object, 1);

            mng.ActivateSeason(season.Id);
        }
    }
}
