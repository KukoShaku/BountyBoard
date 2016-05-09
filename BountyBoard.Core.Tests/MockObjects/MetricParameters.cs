using BountyBoard.Core.Metrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Test.MockObjects
{
    internal class MetricParameters : IMetricParameters
    {
        public string AsData
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Guid Key { get; set; }

        public string UsageText { get; set; }
        
    }
}
