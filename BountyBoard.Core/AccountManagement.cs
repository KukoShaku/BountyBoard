﻿using BountyBoard.Core.Data;
using BountyBoard.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public class AccountManagement : UserRestrictedDatabaseLink
    {
        public const int EmailDays = 30;

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
        public void InvitePerson(PersonInvitation invitation)
        {
            if (invitation == null)
            {
                throw new ArgumentNullException(nameof(invitation));
            }

            if (invitation.AccountGroupId <= 0)
            {
                throw new ArgumentException("Account group id has to be higher than 0", nameof(invitation.AccountGroupId));
            }

            var accountGroup = Context.List<AccountGroup>().Single(x => x.Id == invitation.AccountGroupId);
            if (accountGroup.EndDate.HasValue)
            {
                throw new BusinessLogicException("Cannot add to a disabled group");
            }

            if (!Me.AccountGroupPeople.Any(x => x.AccountGroupId == invitation.AccountGroupId))
            {
                throw new BusinessLogicException("Current user does not belong in this group");
            }

            var existingInvitation = Context.List<Invitation>()
                .SingleOrDefault(x => x.EmailAddress.Equals(invitation.Email, StringComparison.OrdinalIgnoreCase)
                    && x.AccountGroupId == invitation.AccountGroupId);
            if (existingInvitation == null)
            {
                Context.Add(new Invitation
                {
                    AccountGroup = accountGroup,
                    AccountGroupId = invitation.AccountGroupId,
                    EmailAddress = invitation.Email,
                    ExpirationDate = DateTime.Now.AddDays(EmailDays),
                    InvitedById = this.Me.Id,
                    InvitedBy = this.Me,
                    Name = invitation.Name,
                });

            }
            else
            {
                existingInvitation.ExpirationDate = DateTime.Now.AddDays(EmailDays);
            }

            Context.SaveChanges();
            
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
