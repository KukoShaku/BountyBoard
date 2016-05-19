using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Season : DatabaseObject
    {
        internal Season() { }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// All of the achievements associated with this season
        /// </summary>
        public virtual ICollection<Achievement> Achievements { get; set; }

    }
}
