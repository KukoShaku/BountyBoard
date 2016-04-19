using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Person : DatabaseObject
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }

        /// <summary>
        /// Collection of Awarded achievements based on season
        /// </summary>
        public virtual ICollection<AwardedAchievement> Awards { get; set; }
    }
}
