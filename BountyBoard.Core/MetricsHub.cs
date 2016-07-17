using BountyBoard.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BountyBoard.Core.Metrics
{
    /// <summary>
    /// can't think of a beter name for it
    /// The purpose of this is for all metric related queries to end up here.
    /// EG. how many achievements have i done.
    /// All statistical shit needs to go in here
    /// </summary>
    public class MetricsHub : UserRestrictedDatabaseLink
    {

        public MetricsHub(IDatabaseContext context, int personId)
            : base(context, personId)
        {
            _availableMetrics = new List<IMetricDetail>();
        }

        private IList<IMetricDetail> _availableMetrics;
        public IEnumerable<IMetricDetail> AvailableMetrics
        {
            get
            {
                return _availableMetrics;
            }
        }

        public void RegisterMetric(IMetricDetail metric)
        {
            _availableMetrics.Add(metric);
        }

        public void Execute<TParemeters>(TParemeters parameters)
        {
            var metric = AvailableMetrics.Single(x => x.GetType().GenericTypeArguments[0] == parameters.GetType());
            throw new NotImplementedException();
            //the idea here is that we do a assembly lookup
        }
    }
}
