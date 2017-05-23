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
    internal class ByDeviceModel : VisitDimensionBase
    {
        private readonly IDeviceDetectionSettings deviceDetectionSettings;
        private readonly IDeviceInformationService deviceInformationService;
        private readonly ILogger logger;

        public ByDeviceModel(Guid dimensionId) : this(dimensionId, ApiContainerRepositories.GetDeviceInformationService(), CoreContainerRepositories.GetDeviceDetectionSettings())
        {
        }

        public ByDeviceModel(Guid dimensionId, IDeviceInformationService deviceInformationService, IDeviceDetectionSettings deviceDetectionSettings) : base(dimensionId)
        {
            logger = CoreContainer.GetLogger();
            Assert.ArgumentNotNull(deviceDetectionSettings, "deviceDetectionSettings");
            Assert.ArgumentNotNull(deviceInformationService, "deviceInformationService");
            this.deviceInformationService = deviceInformationService;
            this.deviceDetectionSettings = deviceDetectionSettings;
        }

        private string BuildClassificationUnavailableKey()
        {
            var builder = new HierarchicalKeyBuilder();
            builder.Add("DeviceClassificationUnavailable");
            builder.Add("DeviceClassificationUnavailable");
            return builder.ToString();
        }

        public override string GetKey(IVisitAggregationContext context)
        {
            try
            {
                if (!deviceDetectionSettings.IsEnabled)
                {
                    logger.Debug($"Device detection component is disabled). Device model {context.Visit.UserAgent} will be aggregated as classification unavailable. ");
                    return BuildClassificationUnavailableKey();
                }

                if (!deviceDetectionSettings.IsReady)
                {
                    logger.Warn("Device detection component is not ready. Device model will be aggregated as classification unavailable.");
                    return BuildClassificationUnavailableKey();
                }

                var deviceInformation = deviceInformationService.GetDeviceInformation(context.Visit.UserAgent);
                return new HierarchicalKeyBuilder().Add(Enum.GetName(typeof(DeviceType), deviceInformation.DeviceType)).Add(deviceInformation.DeviceModelName).ToString();
            }
            catch (DeviceDetectionException exception)
            {
                logger.Error(exception.Message, exception);
                return BuildClassificationUnavailableKey();
            }
        }

        public override bool HasDimensionKey(IVisitAggregationContext context) => !context.Visit.UserAgent.IsNullOrEmpty();
    }
}
