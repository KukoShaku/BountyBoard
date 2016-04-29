using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class AccountManagement : UserRestrictedDatabaseLink
    {
        public AccountManagement(IDatabaseContext context, int personId)  
            : base (context, personId)
        {

        }

        public IEnumerable<Person> GetMyColleagues(int accountGroupId, bool includeInactive = false)
        {
            return GetMyColleagues(includeInactive).Where(x => x.AccountGroupPeople.Any(y => y.AccountGroupId == accountGroupId));
        }

        private IEnumerable<Person> AllMyColleagues
        {
            get
            {
                return Me.AccountGroupPeople.SelectMany(x => x.AccountGroup.AccountGroupPeople.Select(y => y.Person));
            }
        }

        public IEnumerable<Person> GetMyColleagues(bool includeInactive = false)
        {
            return AllMyColleagues.Where(x => !x.DisabledDate.HasValue || includeInactive).Distinct();
        }

        /// <summary>
        /// This is used to introduce someone to the system
        /// </summary>
        /// <param name="person"></param>
        /// <param name="accountGroupId"></param>
        public void InvitePerson(Person person, int accountGroupId)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (accountGroupId <= 0)
            {
                throw new ArgumentException("Account group id has to be higher than 0", nameof(accountGroupId));
            }

            var accountGroup = Context.List<AccountGroup>().Single(x => x.Id == accountGroupId);
            if (accountGroup.EndDate.HasValue)
            {
                throw new BusinessLogicException("Cannot add to a disabled group");
            }

            if (!Me.AccountGroupPeople.Any(x => x.AccountGroupId == accountGroupId))
            {
                throw new BusinessLogicException("Current user does not belong in this group");
            }

            if (Context.List<AccountGroupPeople>().Any(x => x.PersonId == person.Id && x.AccountGroupId == accountGroupId))
            {
                //early return, already added
                return;
            }
            else
            {
                var join = new AccountGroupPeople 
                {
                    PersonId = person.Id,
                    AccountGroupId = accountGroupId
                };

                Context.Add(join);
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// This disables people's accounts for given account group
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="accountGroupId"></param>
        public void DisableAccount(int personId, int accountGroupId)
        {
            var targetPerson = GetMyColleagues(accountGroupId, true).Single(x => x.Id == personId);
            var join = targetPerson.AccountGroupPeople.SingleOrDefault(x => x.AccountGroupId == accountGroupId);
            if (join != null)
            {
                Context.Delete<AccountGroupPeople>(join.Id);
                Context.SaveChanges();
            }

        }
        /// <summary>
        /// Removes yourself from this org.
        /// </summary>
        /// <param name="accountGroupId"></param>
        public void DisableMyAccount(int accountGroupId)
        {
            DisableAccount(this.Me.Id, accountGroupId);
        }
    }
}
