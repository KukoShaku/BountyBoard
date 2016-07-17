using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public abstract class AccountBasedManagement : UserRestrictedDatabaseLink
    {
        public AccountGroup AccountGroup { get; private set; }
        public PermissionLevel AccountGroupPermission { get; private set; }
        protected AccountBasedManagement(IDatabaseContext context, int personId, int accountId)
            :base(context, personId)
        {
            var permission = MyPermissions.Single(x => x.Key.Id == accountId);
            AccountGroup = permission.Key;
            AccountGroupPermission = permission.Value;

        }
    }
}
