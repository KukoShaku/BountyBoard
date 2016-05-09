using System;

namespace BountyBoard.Core.Metrics
{
    public interface IMetricParameters
    {
        Guid Key { get; }
        string UsageText { get; }

        string AsData { get; }
    }
}