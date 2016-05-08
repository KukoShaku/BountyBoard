using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Metrics
{
    public abstract class MetricDetail<TMetricInput> : IMetricDetail 
        where TMetricInput : IMetricParameters
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// How to make queries
        /// </summary>4
        public TMetricInput MetricParameters { get; set; }

        protected MetricDetail(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
