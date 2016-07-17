using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class ReceivedEvent : DatabaseObject
    {
        public DateTime ReceivedDate { get; set; }
        public string JsonData { get; set; }
        public AccountGroup Group { get; set; }
        public int GroupId { get; set; }
        public DateTime? ProcessedTime { get; set; }
        public string Error { get; set; }
    }
}
