﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class AccountGroup : DatabaseObject
    {
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public virtual ICollection<AccountGroupPeople> AccountGroupPeople { get; set; }

    }
}