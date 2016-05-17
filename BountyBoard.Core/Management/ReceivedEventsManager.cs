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

        private ApiKey GetKey(IMetricParameters parameters)
        {
            return Context.List<ApiKey>().Single(x => x.Key == parameters.Key 
                && x.IsActive 
                && x.ValidUpTo > DateTime.Now);
            //this needs to throw better errors
        }

        internal void AddData(IMetricParameters par)
        {
            var apiKey = GetKey(par);
            Context.Add(new ReceivedEvent
            {
                GroupId = apiKey.AccountGroupId,
                Group = apiKey.AccountGroup,
                ReceivedDate = DateTime.Now,
                JsonData = par.AsData,
                ProcessedTime = null, //explicitly set this 
                Error = null //no errors as of yet
            });
            Context.SaveChanges();
        }
    }
}
