﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    /// <summary>
    /// Joins people to accounts. This is to address people having multiple groups
    /// </summary>
    public class AccountGroupPerson : DatabaseObject
    {
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public AccountGroup AccountGroup { get; set; }
        public int AccountGroupId { get; set; }

        public PermissionLevel PermissionLevel { get; set; }

        /// <summary>
        /// Used for uniquely identifying someone in the account group.
        /// </summary>
        public string CustomKey { get; set; }
    }
}
