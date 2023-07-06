using Meadow;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeadowScheduler.Core
{
    public class SchedulerSettings : ConfigurableObject
    {
        // NTP Settings
        public string NtpServerName => GetConfiguredString(nameof(NtpServerName), "pool.ntp.org");
        public int NtpServerPort => GetConfiguredInt(nameof(NtpServerPort), 123);
        public int NtpTimeoutSeconds => GetConfiguredInt(nameof(NtpTimeoutSeconds), 15);
        public int NtpRetryCount => GetConfiguredInt(nameof(NtpRetryCount), 5);
    }
}
