using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Achievement : DatabaseObject
    {
        internal Achievement() { }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public bool IsApproved { get; set; }
        public double Value { get; set; }
        public Guid Key { get; set; }

        public Season Season { get; set; }
        public int? SeasonId { get; set; }

        //helpers
        public AccountGroup AccountGroup { get; set; }
        public int AccountGroupId { get; set; }

    }
}
