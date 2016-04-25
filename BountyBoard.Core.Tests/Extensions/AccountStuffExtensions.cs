using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.Extensions
{
    internal static class AccountStuffExtensions
    {
        internal static AccountGroupPeople AddToGroup(this Person p, AccountGroup group, bool disabledPermissions = false)
        {
            if (p.AccountGroups == null)
            {
                p.AccountGroups = new List<AccountGroupPeople>();
            }

            var join = new AccountGroupPeople { AccountGroup = group, AccountGroupId = group.Id, Person = p, PersonId = p.Id };
            p.AccountGroups.Add(join);
            if (group.AccountGroupPeople == null)
            {
                group.AccountGroupPeople = new List<AccountGroupPeople>();
            }

            group.AccountGroupPeople.Add(join);

            return join;
        }

    }
}
