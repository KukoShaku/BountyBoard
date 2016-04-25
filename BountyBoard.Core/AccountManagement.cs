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

        public IEnumerable<Person> GetMyColleagues(bool includeInactive = false)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is used to introduce someone to the system
        /// </summary>
        /// <param name="person"></param>
        /// <param name="accountGroupId"></param>
        public void InvitePerson(Person person, int accountGroupId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This disables people's accounts for given account group
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="accountGroupId"></param>
        public void DisableAccount(int personId, int accountGroupId)
        {
            throw new NotImplementedException();
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
