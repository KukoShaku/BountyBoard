using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class PersonWallet : DatabaseObject
    {
        public double Value { get; set; }
        public int PersonId { get; set; }
        public virtual Person Person { get; set; }
        public int AccountGroupId { get; set; }
        public virtual AccountGroup AccountGroup { get; set; }
    }
}
