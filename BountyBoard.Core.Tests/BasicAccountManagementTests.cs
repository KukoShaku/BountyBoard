using BountyBoard.Core.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test
{
    [TestClass]
    public class BasicAccountManagementTests
    {

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void CreateNewAccountGroup_NoAccount_ThrowsException()
        {
            Mock<IDatabaseContext> context = new Mock<IDatabaseContext>();
            BasicAccountManagement mng = new BasicAccountManagement(context.Object);
            
            mng.CreateNewAccountGroup(new ViewModels.NewAccountGroup { AdministratorUserName = "Hello" });
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException))]
        public void CreateNewAccountGroup_NoUserName_ThrowsException()
        {
            Mock<IDatabaseContext> context = new Mock<IDatabaseContext>();
            BasicAccountManagement mng = new BasicAccountManagement(context.Object);

            mng.CreateNewAccountGroup(new ViewModels.NewAccountGroup { GroupName = "Hello" });
        }

        [TestMethod]
        public void CreateNewAccountGroup_Normal_SavesChanges()
        {
            Mock<IDatabaseContext> context = new Mock<IDatabaseContext>();
            BasicAccountManagement mng = new BasicAccountManagement(context.Object);

            mng.CreateNewAccountGroup(new ViewModels.NewAccountGroup { GroupName = "Hello", AdministratorUserName= "Yes" });

            context.Verify(x => x.SaveChanges());
            context.Verify(x => x.Add<Person>(It.Is<Person>(y => y.Name == "Yes")));
            context.Verify(x => x.Add(It.Is<AccountGroup>(y => y.Name == "Hello")));
        }
    }
}
