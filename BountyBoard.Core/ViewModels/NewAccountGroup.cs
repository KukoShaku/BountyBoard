using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.ViewModels
{
    public class NewAccountGroup
    {
        public string GroupName { get; set; }
        public string AdministratorUserName { get; set; }
        public string PersonDescription { get; internal set; }
        public string Email { get; internal set; }
    }
}
