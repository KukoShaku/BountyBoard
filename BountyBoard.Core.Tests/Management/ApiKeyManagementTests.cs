using BountyBoard.Core.Data;
using BountyBoard.Core.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BountyBoard.Core.Test.Extensions;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class ApiKeyManagementTests
    {
        private ApiKeyManagement Resolve(Mock<IDatabaseContext> fakeContext, int currentUser)
        {

            var person = new Person
            {
                Id = currentUser,

            };

            var defaultGroup = new AccountGroup
            {
                Id = 1
            };

            var join = person.AddToGroup(defaultGroup, 1);
            join.PermissionLevel = PermissionLevel.Admin; //defaults to this

            fakeContext.Setup(x => x.List<Person>()).Returns(new[] 
            {
                person
            }.AsQueryable());


            var result = new ApiKeyManagement(fakeContext.Object, currentUser);
            return result;
        }

        [TestMethod, TestCategory("Audit")]
        public void CreateKey_GetsYourNameAttached()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mng = Resolve(fakeContext, 1);

            mng.CreateKey();

            fakeContext.Verify(x => x.SaveChanges(), Times.Once);
            fakeContext.Verify(x => x.Add(It.Is<ApiKey>(y => y.CreatedById == 1)), Times.Once);
        }

        [TestMethod, TestCategory("Security")]
        public void CreateKey_KeyIsNotGuidEmpty()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mng = Resolve(fakeContext, 1);

            mng.CreateKey();

            fakeContext.Verify(x => x.Add(It.Is<ApiKey>(y => y.Key == Guid.Empty)), Times.Never);
            fakeContext.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod, ExpectedException(typeof(BusinessLogicException)), TestCategory("Secuity")]
        public void CreateKey_NormalUser_Fails()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mng = Resolve(fakeContext, 1);

            mng.CreateKey();


        }

        [TestMethod]
        public void RegenerateKey_DifferentKey()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var mng = Resolve(fakeContext, 1);
            Guid oldKey = Guid.NewGuid();
            fakeContext.Setup(x => x.List<ApiKey>())
                .Returns(new[] { new ApiKey
                {
                    AccountGroupId = 1,
                    Key = oldKey,
                }
            }.AsQueryable());

            mng.RegenerateKey(100);

            fakeContext.Verify(x => x.SaveChanges(), Times.Once);
            fakeContext.Verify(x => x.Update(It.Is<ApiKey>(y => y.Key != oldKey)));
        }

        
    }
}
