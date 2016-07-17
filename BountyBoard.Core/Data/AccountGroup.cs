using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{
    public class AccountGroup : DatabaseObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        public virtual ICollection<Season> Seasons { get; set; }
        public virtual ICollection<AccountGroupPerson> AccountGroupPeople { get; set; }
        public virtual ICollection<ApiKey> ApiKeys { get; set; }
        public virtual ICollection<PersonWallet> Wallets { get; set; }
        public virtual ICollection<Achievement> Achievements { get; set; }

        public Season CurrentActiveSeason
        {
            get
            {
                return Seasons.SingleOrDefault(x => x.IsActive && x.IsBetween(DateTime.Now));
            }
        }

        internal bool HasPerson(Person me)
        {
            return AccountGroupPeople.Any(x => x.PersonId == me.Id);
        }
    }
}
