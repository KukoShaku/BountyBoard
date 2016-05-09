﻿using BountyBoard.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    public abstract class UserRestrictedDatabaseLink : DatabaseLink
    {
        private readonly Person _person;
        public Person Me { get { return _person; } }
        public bool ValidUser { get { return !_person.DisabledDate.HasValue; } }

        public IEnumerable<KeyValuePair<AccountGroup, PermissionLevel>> MyPermissions
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        protected UserRestrictedDatabaseLink(IDatabaseContext context, int personId) 
            : base(context)
        {
            //TODO: Only admins can reactivate their account.
            _person = Context.List<Person>().Single(x => 
                x.Id == personId 
                && !x.DisabledDate.HasValue);
        }
    }
}
