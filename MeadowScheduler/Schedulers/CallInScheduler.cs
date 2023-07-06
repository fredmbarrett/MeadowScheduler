using MeadowScheduler.Core;
using MeadowScheduler.Core.Enums;
using MeadowScheduler.Domain;
using System;
using System.Threading;

namespace MeadowScheduler.Schedulers
{
    internal class CallInScheduler : SchedulerBase
    {
        private static CallInScheduler _instance;
        public static CallInScheduler Instance => _instance ??= new CallInScheduler();

        private CallInScheduler() { }

        public void CreateCallInTask(CallInSchedule schedule, Action<CallInSchedule> task)
        {
            if (schedule.ScheduleType == ScheduleType.RecurringEvent)
            {
                if (schedule.ActionHour > 0) 
                { 
                    CreatePerHourIntervalTask(schedule, task); 
                }
                else 
                { 
                    CreatePerMinuteIntervalTask(schedule, task); 
                }
            }
            else
            {
                var interval = new TimeSpan(schedule.ActionHour, schedule.ActionMinute, 0);
                var timeToGo = DateTime.Today + interval - DateTime.Now;
                if (timeToGo < TimeSpan.Zero)
                    timeToGo = timeToGo.Add(new TimeSpan(24, 0, 0));

                CreateScheduleTimer(schedule, interval, 0, task);
            }
        }

        /// <summary>
        /// Creates a per-n-minute interval call in action
        /// </summary>
        /// <param name="schedule">CallInSchedule</param>
        /// <param name="task">Action to perform</param>
        void CreatePerMinuteIntervalTask(CallInSchedule schedule, Action<CallInSchedule> task)
        {
            var interval = TimeSpan.FromMinutes(schedule.ActionMinute);
            CreateScheduleTimer(schedule, interval, 0, task);
        }

        /// <summary>
        /// Creates a per-n-hour interval call in action,
        /// with optional "n-minutes after the hour" adjustment
        /// </summary>
        /// <param name="schedule">CallInSchedule</param>
        /// <param name="task">Action to perform</param>
        void CreatePerHourIntervalTask(CallInSchedule schedule, Action<CallInSchedule> task)
        {
            var interval = TimeSpan.FromHours(schedule.ActionHour);
            var delay = schedule.ActionMinute;
            CreateScheduleTimer(schedule, interval, delay, task);
        }

        void CreateScheduleTimer(CallInSchedule schedule, TimeSpan interval, int delayMinutes, Action<CallInSchedule> task)
        {
            var now = DateTime.Now;

            var startTime = now.RoundUp(interval).AddMinutes(delayMinutes);
            var timeToGo = startTime - now;

            var timer = new Timer(x =>
            {
                task.Invoke(schedule);
            }, null, timeToGo, interval);

            Timers.Add(timer);
        }
    }
}
