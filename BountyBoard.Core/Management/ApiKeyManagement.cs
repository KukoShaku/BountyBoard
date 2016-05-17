using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class ApiKeyManagement : AccountBasedManagement
    {
        //TODO: Change the implementation later
        private const int DefaultApiKeyValidDays = 356;
        public ApiKeyManagement(IDatabaseContext context, int personId, int accountId)
            :base(context, personId, accountId)
        {
            if (!CanDoStuff)
            {
                throw new UnauthorizedAccessException("You are not an administrative user.");
            }
        }

        private bool CanDoStuff
        {
            get
            {
                return AccountGroupPermission == PermissionLevel.Admin || AccountGroupPermission == PermissionLevel.SuperAdmin;
            }
        }

        public void CreateKey(string description)
        {
            if (CanDoStuff)
            {
                Context.Add(new ApiKey
                {
                    CreatedBy = Me,
                    CreatedById = Me.Id,
                    Description = "",
                    CreatedTime = DateTime.Now,
                    AccountGroup = AccountGroup,
                    AccountGroupId = AccountGroup.Id,
                    IsActive = true,
                    Key = Guid.NewGuid(),
                    ValidUpTo = DateTime.Now.AddDays(DefaultApiKeyValidDays)
                });
                Context.SaveChanges();
            }
            else
            {
                throw new UnauthorizedAccessException("You currently do not have any permissions to create a key. Please ask your team leader or manager if you'd like a key.");
            }
        }

        public IEnumerable<ApiKey> GetKeys
        {
            get
            {
                return Context.List<ApiKey>().Where(x => x.AccountGroupId == AccountGroup.Id);
            }
        }

        /// <summary>
        /// Rerolls a new key. 
        /// </summary>
        /// <param name="keyId"></param>
        public void RegenerateKey(int keyId)
        {
            var apiKey = GetKeys.Single(x => x.Id == keyId); 
            apiKey.Key = Guid.NewGuid();
            Context.SaveChanges();

        }
    }
}
