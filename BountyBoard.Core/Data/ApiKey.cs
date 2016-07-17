using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class ApiKey : DatabaseObject
    {
        public string Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ValidUpTo { get; set; }
        public bool IsActive { get; set; }
        public Guid Key { get; set; }

        public int AccountGroupId { get; set; }
        public virtual AccountGroup AccountGroup { get; set; }

        public virtual Person CreatedBy { get; set; }
        public int CreatedById { get; set; }
    }
}
