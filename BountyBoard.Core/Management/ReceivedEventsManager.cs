using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BountyBoard.Core.Metrics;
using BountyBoard.Core.Data;

namespace BountyBoard.Core.Management
{
    public class ReceivedEventsManager : DatabaseLink
    {
        public ReceivedEventsManager(IDatabaseContext context)
            : base(context)
        {

        }

        private void Validate(IMetricParameters parameters)
        {
            Context.List<ApiKey>().Single(x => x.Key == parameters.Key 
                && x.IsActive 
                && x.ValidUpTo > DateTime.Now);
        }

        internal void AddData(IMetricParameters par)
        {
            Validate(par);
            throw new NotImplementedException();
        }
    }
}
