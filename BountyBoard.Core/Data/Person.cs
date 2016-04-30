using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Person : DatabaseObject
    {
        internal Person() { }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime? DisabledDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public bool AccountValidated { get; set; }

        /// <summary>
        /// For Popular people
        /// </summary>
        public virtual ICollection<AccountGroupPeople> AccountGroupPeople { get; set; }

        /// <summary>
        /// Collection of Awarded achievements based on season
        /// </summary>
        public virtual ICollection<AwardedAchievement> Awards { get; set; }
    }
}
