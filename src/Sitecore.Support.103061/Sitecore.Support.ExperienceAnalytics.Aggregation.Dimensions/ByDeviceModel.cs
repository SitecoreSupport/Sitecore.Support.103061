using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.CES.DeviceDetection;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using Sitecore.ExperienceAnalytics.Core;


namespace Sitecore.Support.ExperienceAnalytics.Aggregation.Dimensions
{
  internal class ByDeviceModel : VisitDimensionBase
  {
    public ByDeviceModel(Guid dimensionId) : base(dimensionId)
    {
    }

    public override string GetKey(IVisitAggregationContext context)
    {
      if (DeviceDetectionManager.IsEnabled)
        Log.Warn("Sitecore.support.103061: Device Detection is enabled, please remove the patch by disabling the Sitecore.Support.103061.config file.", this);
      return BuildClassificationUnavailableKey();
    }

    public override bool HasDimensionKey(IVisitAggregationContext context)
    {
      return false;
    }
    private string BuildClassificationUnavailableKey()
    {
      HierarchicalKeyBuilder hierarchicalKeyBuilder = new HierarchicalKeyBuilder();
      hierarchicalKeyBuilder.Add("DeviceClassificationUnavailable");
      hierarchicalKeyBuilder.Add("DeviceClassificationUnavailable");
      return hierarchicalKeyBuilder.ToString();
    }
  }
}