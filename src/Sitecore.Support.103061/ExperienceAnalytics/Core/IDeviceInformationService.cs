#region Usings

using Sitecore.CES.DeviceDetection;

#endregion

namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal interface IDeviceInformationService
    {
        DeviceInformation GetDeviceInformation(string userAgent);
    }
}
