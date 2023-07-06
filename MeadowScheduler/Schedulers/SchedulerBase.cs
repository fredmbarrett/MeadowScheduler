using Meadow;
using Meadow.Logging;
using MeadowScheduler.Core;
using System.Collections.Generic;
using System.Threading;

namespace MeadowScheduler.Schedulers
{
    public abstract class SchedulerBase
    {
        protected SchedulerSettings AppSettings => (SchedulerSettings)Resolver.Services.Get(typeof(SchedulerSettings));
        protected static Logger Log { get => Resolver.Log; }

        public List<Timer> Timers
        {
            get => timers ??= new List<Timer>();
            protected set
            {
                timers = value;
            }
        }
        private List<Timer> timers = new List<Timer>();

        /// <summary>
        /// Dispose all active timers and reset the collection
        /// </summary>
        public void ClearTimers()
        {
            foreach (var t in Timers)
            {
                t.Dispose();
            }

            timers = new List<Timer>();
        }
    }
}
