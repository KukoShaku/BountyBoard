using BountyBoard.Core.Data;
using BountyBoard.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core
{
    /// <summary>
    /// Basic account management for the unregistered
    /// </summary>
    public class BasicAccountManagement : DatabaseLink
    {
        public BasicAccountManagement(IDatabaseContext context) : base(context)
        {

        }

        public void CreateNewAccountGroup(NewAccountGroup newAccountGroup)
        {
            if (newAccountGroup == null)
            {
                throw new ArgumentNullException(nameof(newAccountGroup));
            }

            if (String.IsNullOrEmpty(newAccountGroup.GroupName))
            {
                throw new BusinessLogicException("Input a group name");
            }

            if (String.IsNullOrEmpty(newAccountGroup.AdministratorUserName))
            {
                throw new BusinessLogicException("Who will you be known as?");
            }

            var accountGroup = new AccountGroup
            {
                CreatedDate = DateTime.Now,
                Name = newAccountGroup.GroupName,
            };

            var person = new Person
            {
                Name = accountGroup.Name,
                Description = newAccountGroup.PersonDescription,
                CreatedDate = DateTime.Now,
                Email = newAccountGroup.Email
            };

            Context.Add(person);
            Context.Add(accountGroup);
            Context.SaveChanges();
        }
    }
}
