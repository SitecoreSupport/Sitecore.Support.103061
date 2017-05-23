#region Usings

using System;
using Sitecore.CES.DeviceDetection;
using Sitecore.Diagnostics;

#endregion

namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal class DeviceInformationService : IDeviceInformationService
    {
        internal Func<string, DeviceInformation> UserAgentDetector = s => DeviceDetectionManager.GetDeviceInformation(s);

        public DeviceInformation GetDeviceInformation(string userAgent)
        {
            Assert.ArgumentNotNullOrEmpty(userAgent, "userAgent");
            var information = UserAgentDetector(userAgent);
            if (information == null)
            {
                throw new InvalidOperationException($"No device information available for user agent ({userAgent}). Device Detection Api retured null.");
            }
            return information;
        }
    }
}
