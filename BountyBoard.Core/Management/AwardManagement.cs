﻿using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class AwardManagement : CrudLink<AwardedAchievement>
    {
        public AwardManagement(IDatabaseContext context) 
            : base(context) { }

    }
}
