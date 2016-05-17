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
                return "This is some test json data";
            }
        }

        public Guid Key { get; set; }

        public string UsageText { get; set; }
        
        public MetricParameters(Guid key)
        {
            Key = key;
        }

    }
}
