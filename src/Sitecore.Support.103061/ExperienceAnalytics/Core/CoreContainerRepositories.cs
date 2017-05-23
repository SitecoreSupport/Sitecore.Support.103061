namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal class CoreContainerRepositories
    {
        private static IDeviceDetectionSettings deviceDetectionSettings;

        internal static IDeviceDetectionSettings DeviceDetectionSettings
        {
            get
            {
                return (deviceDetectionSettings) ?? (deviceDetectionSettings = new DeviceDetectionSettings());
            }
            set
            {
                deviceDetectionSettings = value;
            }
        }

        internal static IDeviceDetectionSettings GetDeviceDetectionSettings() => DeviceDetectionSettings;
    }
}
