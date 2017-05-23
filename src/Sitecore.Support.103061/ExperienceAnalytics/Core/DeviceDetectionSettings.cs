#region Usings

using System;
using Sitecore.CES.DeviceDetection;

#endregion

namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal sealed class DeviceDetectionSettings : IDeviceDetectionSettings
    {
        public bool CheckInitialization() =>
            DeviceDetectionManager.CheckInitialization();

        public bool CheckInitialization(TimeSpan timeOut) =>
            DeviceDetectionManager.CheckInitialization(timeOut);

        public bool IsEnabled =>
            DeviceDetectionManager.IsEnabled;

        public bool IsReady =>
            DeviceDetectionManager.IsReady;
    }
}
