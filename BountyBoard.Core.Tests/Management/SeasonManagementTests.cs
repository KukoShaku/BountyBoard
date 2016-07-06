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

            throw new NotImplementedException();
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Season Admin")]
        public void ActivateSeason_InvalidEndDate_ThrowsException()
        {
            var fakeContext = new Mock<IDatabaseContext>();
            Person person = new Person
            {
                Id = 1,
            };
            var accountGroupPeople = new List<AccountGroupPeople>()
            {
                new AccountGroupPeople
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
