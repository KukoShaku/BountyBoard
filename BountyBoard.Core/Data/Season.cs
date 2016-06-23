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

        public AccountGroup AccountGroup { get; set; }
        public int AccountGroupId { get; set; }

        /// <summary>
        /// Start date is not inclusive.
        /// End date is inclusive
        /// </summary>
        /// <param name="time"></param>
        /// <returns>true if time is within start and end date. If either are null, returns false</returns>
        internal bool IsBetween(DateTime time)
        {
            if (StartDate == null || EndDate == null)
            {
                return false;
            }
            else
            {
                return StartDate < time && time <= EndDate;
            }
        }

    }
}
