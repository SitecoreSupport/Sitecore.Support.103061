#region Usings

using System;

#endregion

namespace Sitecore.Support.ExperienceAnalytics.Core
{
    internal interface IDeviceDetectionSettings
    {
        bool IsEnabled { get; }

        bool IsReady { get; }
        bool CheckInitialization();
        bool CheckInitialization(TimeSpan timeOut);
    }
}
