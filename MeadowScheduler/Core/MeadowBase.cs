using Meadow;
using Meadow.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeadowScheduler.Core
{
    internal abstract class MeadowBase
    {
        /// <summary>
        /// Local application settings from app.config.yaml
        /// </summary>
        protected SchedulerSettings AppSettings = (SchedulerSettings)Resolver.Services.Get(typeof(SchedulerSettings));
        /// <summary>
        /// Meadow hardware reference
        /// </summary>
        protected IMeadowDevice MeadowDevice { get; } = Resolver.Device;
        /// <summary>
        /// Meadow Logger reference (Resolver.Log
        /// </summary>
        protected Logger Log { get; } = Resolver.Log;

        public MeadowBase() { }
    }
}
