using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Role : DatabaseObject
    {
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }

    }
}
