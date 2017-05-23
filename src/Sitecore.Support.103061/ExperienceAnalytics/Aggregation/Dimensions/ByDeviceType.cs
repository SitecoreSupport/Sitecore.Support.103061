#region Usings

using System;
using Sitecore.Analytics.Aggregation.Data.Model;
using Sitecore.CES.DeviceDetection;
using Sitecore.CES.DeviceDetection.Exceptions;
using Sitecore.Diagnostics;
using Sitecore.ExperienceAnalytics.Aggregation.Dimensions;
using Sitecore.ExperienceAnalytics.Core;
using Sitecore.ExperienceAnalytics.Core.Diagnostics;
using Sitecore.StringExtensions;
using Sitecore.Support.ExperienceAnalytics.Core;

#endregion

namespace Sitecore.Support.ExperienceAnalytics.Aggregation.Dimensions
{
    internal class ByDeviceType : VisitDimensionBase
    {
        private readonly IDeviceDetectionSettings deviceDetectionSettings;
        private readonly IDeviceInformationService deviceInformationService;
        private readonly ILogger logger;

        public ByDeviceType(Guid dimensionId) : this(dimensionId, ApiContainerRepositories.GetDeviceInformationService(), CoreContainerRepositories.GetDeviceDetectionSettings())
        {
        }


        public ByDeviceType(Guid dimensionId, IDeviceInformationService deviceInformationService, IDeviceDetectionSettings deviceDetectionSettings) : base(dimensionId)
        {
            logger = CoreContainer.GetLogger();
            Assert.ArgumentNotNull(deviceDetectionSettings, "deviceDetectionSettings");
            Assert.ArgumentNotNull(deviceInformationService, "deviceInformationService");
            this.deviceDetectionSettings = deviceDetectionSettings;
            this.deviceInformationService = deviceInformationService;
        }

        private string GetClassificationUnavailableKey()
        {
            var builder = new HierarchicalKeyBuilder();
            builder.Add("DeviceClassificationUnavailable");
            return builder.ToString();
        }

        public override string GetKey(IVisitAggregationContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            try
            {
                if (!deviceDetectionSettings.IsEnabled)
                {
                    logger.Debug($"Device detection component is disabled). Device model {context.Visit.UserAgent} will be aggregated as classification unavailable. ");
                    return GetClassificationUnavailableKey();
                }

                if (!deviceDetectionSettings.IsReady)
                {
                    logger.Warn("Device detection component is not ready. Device model will be aggregated as classification unavailable.");
                    return GetClassificationUnavailableKey();
                }

                var deviceInformation = deviceInformationService.GetDeviceInformation(context.Visit.UserAgent);
                return new HierarchicalKeyBuilder().Add(Enum.GetName(typeof(DeviceType), deviceInformation.DeviceType)).ToString();
            }
            catch (DeviceDetectionException exception)
            {
                logger.Error(exception.Message, exception);
                return GetClassificationUnavailableKey();
            }
        }

        public override bool HasDimensionKey(IVisitAggregationContext context)
        {
            Assert.ArgumentNotNull(context, "context");
            return !context.Visit.UserAgent.IsNullOrEmpty();
        }
    }
}
