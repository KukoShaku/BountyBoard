using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Call this if you're unsure if the input exists. It will default to 0 if you do not specify
        /// </summary>
        /// <param name="input"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public static int TryConvert(this string input, int fallback = 0)
        {
            int result = fallback;
            int.TryParse(input, out result);
            return result;
        }
    }
}
