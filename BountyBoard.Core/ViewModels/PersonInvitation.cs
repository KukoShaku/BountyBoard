using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.ViewModels
{
    public class PersonInvitation
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public int AccountGroupId { get; internal set; }
    }
}
