using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.ViewModels
{

    /// <summary>
    /// This is an API class, properties are used to determine what the appropriate targets
    /// are
    /// This is in no way very secure, I'm still currently working on a way to deal with that
    /// It's relatively low impact so meh?
    /// It also serves as a validation center
    /// </summary>
    public class AchievementAllocation
    {
        /// <summary>
        /// Serves no purpose at the moment, not sure what I want to do with this
        /// </summary>
        public bool IsManual { get; set; }
        /// <summary>
        /// this is the ID of the person in the organisation.
        /// It has to be unique to the account-person join.
        /// </summary>
        public string CustomPersonKey { get; set; }

        /// <summary>
        /// The key of the achievement, there will be several achievements with the same key
        /// but only one "active" one at any given time.
        /// If this achievement is not active, some form of override needs to be set
        /// Possibly with the "isManual" property. Uncertain
        /// </summary>
        public Guid AchievementKey { get; set; }

        /// <summary>
        /// Hidden property which will get set with the constructor
        /// </summary>
        public DateTime CreatedDate { get; private set; }
        
        /// <summary>
        /// What's the api key of yours?
        /// </summary>
        public Guid ApiKey { get; set; }

        internal void Validate()
        {
            if (ApiKey == Guid.Empty)
            {
                throw new ArgumentException("Bro, you have no api key", nameof(ApiKey));
            }

            if (String.IsNullOrEmpty(CustomPersonKey))
            {
                throw new ArgumentNullException(nameof(CustomPersonKey));
            }

            if (AchievementKey == Guid.Empty)
            {
                throw new ArgumentException("What achievement are you trying to give?", nameof(AchievementKey));
            }

        }

        public AchievementAllocation()
        {
            CreatedDate = DateTime.Now;
        }
        
    }
}
