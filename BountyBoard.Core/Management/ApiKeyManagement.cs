using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class ApiKeyManagement : UserRestrictedDatabaseLink
    {
        public ApiKeyManagement(IDatabaseContext context, int personId)
            :base(context, personId)
        {
            throw new NotImplementedException();
        }

        public void CreateKey()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApiKey> GetKeys
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Rerolls a new key. 
        /// </summary>
        /// <param name="keyId"></param>
        public void RegenerateKey(int keyId)
        {
            throw new NotImplementedException("");
        }
    }
}
