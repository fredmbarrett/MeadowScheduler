using System;

namespace MeadowScheduler.Core
{
    internal static class SchedulerExtensions
    {
        /// <summary>
        /// Rounds a DateTime up to the next nearest minute
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ts"></param>
        /// <returns></returns>
        public static DateTime RoundUpMinutes(this DateTime dt, TimeSpan ts)
        {
            return new DateTime((dt.Ticks + ts.Ticks - 1) / ts.Ticks * ts.Ticks, dt.Kind);
        }

        public static DateTime RoundUp(this DateTime dt, TimeSpan ts)
        {
            var modTicks = dt.Ticks % ts.Ticks;
            var delta = modTicks != 0 ? ts.Ticks - modTicks : 0;
            return new DateTime(dt.Ticks + delta, dt.Kind);
        }
    }
}
