using BountyBoard.Core.Data;
using BountyBoard.Core.Management;
using BountyBoard.Core.Metrics;
using BountyBoard.Core.Test.MockObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.Management
{
    [TestClass]
    public class ReceivedEventsManagerTests
    {
        private ReceivedEventsManager Resolve(Mock<IDatabaseContext> fakeContext)
        {
            return new ReceivedEventsManager(fakeContext.Object);
        }

        [TestMethod]
        public void AddData_BasicCorrect_AddsToDatabase()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var manager = Resolve(fakeContext);

            Guid key = Guid.NewGuid();
            int groupId = 100;
            fakeContext.Setup(x => x.List<ApiKey>())
                .Returns(new[] 
                {
                    new ApiKey
                    {
                        Key = key,
                        AccountGroupId = groupId,
                        IsActive = true,
                        ValidUpTo = DateTime.MaxValue,
                    }
                }.AsQueryable());
            IMetricParameters par = new MetricParameters();
            manager.AddData(par);
            
            fakeContext.Verify(x => x.Add(It.Is<ReceivedEvent>(y => y.ReceivedDate.Date == DateTime.Today
                && y.GroupId == groupId
                && y.JsonData == par.AsData
                && y.ProcessedTime == null)));
            fakeContext.Verify(x => x.SaveChanges());
        }

        [TestMethod, TestCategory("Security"), ExpectedException(typeof(InvalidOperationException))]
        public void AddData_BadValidationDate_NoSave()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var manager = Resolve(fakeContext);

            Guid key = Guid.NewGuid();
            int groupId = 100;
            fakeContext.Setup(x => x.List<ApiKey>())
                .Returns(new[]
                {
                    new ApiKey
                    {
                        Key = key,
                        AccountGroupId = groupId,
                        IsActive = true,
                        ValidUpTo = DateTime.Today,
                    }
                }.AsQueryable());

            IMetricParameters par = new MetricParameters();
            manager.AddData(par);
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException)), TestCategory("Security")]
        public void AddData_NoKey_ThrowsException()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            var manager = Resolve(fakeContext);

            IMetricParameters par = new MetricParameters();
            manager.AddData(par);
        }

    }
}
