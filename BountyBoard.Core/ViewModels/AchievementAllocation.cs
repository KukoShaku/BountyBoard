using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.ViewModels
{

    /// <summary>
    /// This is a bridging class, it allows data to be transferred appropriately
    /// It also serves as a validation center
    /// </summary>
    public class AchievementAllocation
    {
        public bool IsManual { get; set; }
        public Person SourcePerson { get; set; }
        public Guid AchievementKey { get; set; }
        public DateTime CreatedDate { get; private set; }
        
        public AchievementAllocation()
        {
            CreatedDate = DateTime.Now;
        }
        
    }
}
