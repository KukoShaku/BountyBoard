using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.DataExtensions
{
    public static class PersonExtensions
    {
        /// <summary>
        /// Gets the top 5 achievmeents belonging to this person of all time if season is unspecified
        /// </summary>
        /// <param name="person">The person</param>
        /// <param name="seasonId">If not set, it will grab top 5</param>
        /// <returns></returns>
        internal static IEnumerable<object> GetTop5Achievements(this Person person, int? seasonId = null)
        {
            //explore the person object for clues?
            throw new NotImplementedException();
        }
    }
}
