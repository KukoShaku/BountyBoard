using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Management
{
    public class SeasonManagement : UserRestrictedDatabaseLink
    {
        public SeasonManagement(IDatabaseContext context, int personId) 
            : base(context, personId)
        {

        }

        public IQueryable<Season> List
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        internal void ActivateSeason(int id)
        {
            var season = Context.List<Season>().Single(x => x.Id == id);
            var permission = Me.GetPermissionLevel(season.AccountGroupId);
            if (permission != PermissionLevel.Admin && permission != PermissionLevel.SuperAdmin)
            {
                throw new UnauthorizedAccessException("You do not have the right administration level to modify seasons");
            }
            if (!season.AccountGroup.HasPerson(Me))
            {
                throw new UnauthorizedAccessException("You have no authority to do this");
            }
            if (!season.CanActivate)
            {
                throw new BusinessLogicException("Season has incorrect dates, please ensure season has start and end dates");
            }

            season.IsActive = true;
            Context.SaveChanges();
        }
    }
}
