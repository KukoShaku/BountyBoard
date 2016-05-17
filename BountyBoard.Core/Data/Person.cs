using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class Person : DatabaseObject
    {
        internal Person() { }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime? DisabledDate { get; set; }
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Details is stored however 
        /// it's essentially just a massive json string about the user
        /// </summary>
        public string DetailsJson { get; set; }

        public bool AccountValidated { get; set; }

        /// <summary>
        /// For Popular people
        /// </summary>
        public virtual ICollection<AccountGroupPeople> AccountGroupPeople { get; set; }

        /// <summary>
        /// Collection of Awarded achievements based on season
        /// </summary>
        public virtual ICollection<AwardedAchievement> Awards { get; set; }

        /// <summary>
        /// 0 to many people this person has invited into the game
        /// Used during invitation and sendouts
        /// </summary>
        public virtual ICollection<Invitation> Invitations { get; set; }

        /// <summary>
        /// Keys that this person has created.
        /// </summary>
        public virtual ICollection<ApiKey> CreatedKeys { get; set; }

        /// <summary>
        /// this is a person's wallet, it stores how much "money"
        /// the person currently has.
        /// </summary>
        public virtual ICollection<PersonWallet> Wallets { get; set; }
    }
}
