using BountyBoard.Core.Data;
using BountyBoard.Core.Metrics;
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
    public class MetricsHubTests
    {
        [TestMethod, TestCategory("Metrics")]
        public void AvailableMetrics_ReturnsResults()
        {
            Mock<IDatabaseContext> fakeContext = new Mock<IDatabaseContext>();
            fakeContext.Setup(x => x.List<Person>()).Returns(new[] { new Person
            {
                Id = 1,
            } }.AsQueryable());

            var hub = new MetricsHub(fakeContext.Object, 1);

            var metrics = hub.AvailableMetrics;
            Assert.IsTrue(metrics.Any());
        }
    }
}
