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
        /// <summary>
        /// creates a normal grouped user. override if you want to use it
        /// </summary>
        /// <param name="p"></param>
        /// <param name="group"></param>
        /// <param name="joinId"></param>
        /// <returns></returns>
        internal static AccountGroupPerson AddToGroup(this Person p, AccountGroup group, int joinId)
        {
            if (p.AccountGroupPeople == null)
            {
                p.AccountGroupPeople = new List<AccountGroupPerson>();
            }

            var join = new AccountGroupPerson { AccountGroup = group, AccountGroupId = group.Id, Person = p, PersonId = p.Id };
            p.AccountGroupPeople.Add(join);
            if (group.AccountGroupPeople == null)
            {
                group.AccountGroupPeople = new List<AccountGroupPerson>();
            }

            group.AccountGroupPeople.Add(join);
            join.Id = joinId;
            join.PermissionLevel = PermissionLevel.Normal;
            return join;
        }

    }
}
