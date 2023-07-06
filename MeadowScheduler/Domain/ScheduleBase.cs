using MeadowScheduler.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MeadowScheduler.Domain
{
    internal abstract class ScheduleBase
    {
        /// <summary>
        /// Entity Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Schedule type (Recurring or Single Event)
        /// </summary>
        public ScheduleType ScheduleType { get; set; }

        /// <summary>
        /// Bitmap days of the week this schedule is active:
        /// 0x01 = Sunday
        /// 0x02 = Monday
        /// 0x04 = Tuesday
        /// 0x08 = Wednesday
        /// 0x10 = Thursday
        /// 0x20 = Friday
        /// 0x40 = Saturday
        /// </summary>
        public int ActionDays { get; set; }

        /// <summary>
        /// Action type indicator (meaning determined by inheriting class)
        /// </summary>
        public int ActionType { get; set; }

        /// <summary>
        /// If ScheduleType is "RecurringEvent", then hour value represents
        /// how often the schedule is to occur (e.g. every xx hours).
        /// If ScheduleType is "SingleEvent", then hour value represents
        /// the hour of the day the event is to occur (24-hour format).
        /// </summary>
        public int ActionHour { get; set; }

        /// <summary>
        /// If ScheduleType is "RecurringEvent", then minute value represents
        /// how often the schedule is to occur (e.g. every xx minutes). If ActionHour
        /// value is also set, then minute value represents the number of
        /// minutes after the hour the schedule is to occur (e.g. every 2 hours 
        /// at 5 minutes past the hour).
        /// If ScheduleType is "SingleEvent", then minute value represents the
        /// minutes past the hour specified the event is to occur.
        /// </summary>
        public int ActionMinute { get; set; }

        /// <summary>
        /// Flag indicating whether the schedule is active or not.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
