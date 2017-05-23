namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal class ApiContainerRepositories
    {
        internal static IDeviceInformationService GetDeviceInformationService() =>
        new DeviceInformationService();
    }
}
