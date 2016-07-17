using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Data
{

    /// <summary>
    /// Invitations are sent to an email address. 
    /// Invitations expire
    /// </summary>
    public class Invitation : DatabaseObject
    {
        public DateTime ExpirationDate { get; set; }
        public string EmailAddress { get; set; }
        public string Name { get; set; }
        public virtual Person InvitedBy { get; set; }
        public int InvitedById { get; set; }
        public AccountGroup AccountGroup { get; set; }
        public int AccountGroupId { get; set; }

    }
}
