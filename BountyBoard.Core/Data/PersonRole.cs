using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class PersonRole : DatabaseObject
    {
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public Role Role { get; set; }
        public int RoleId { get; set; }
        public Person IssuedByPerson { get; set; }
        public int IssuedByPersonId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
