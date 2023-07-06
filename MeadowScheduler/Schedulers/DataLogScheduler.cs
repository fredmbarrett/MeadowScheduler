using MeadowScheduler.Core;
using MeadowScheduler.Domain;
using System;
using System.Threading;

namespace MeadowScheduler.Schedulers
{
    internal class DataLogScheduler : SchedulerBase
    {
        private static DataLogScheduler _instance;
        public static DataLogScheduler Instance => _instance ??= new DataLogScheduler();

        public void CreateDataLogTask(DataLogSchedule schedule, Action<DataLogSchedule> task)
        {
            if (schedule.ActionSecond > 0)
            {
                CreatePerSecondIntervalTask(schedule, task);
            }
            else if (schedule.ActionHour > 0 && schedule.ActionMinute > 0)
            {
                CreatePerHourIntervalTask(schedule, task);
            }
            else if (schedule.ActionMinute > 0)
            {
                CreatePerMinuteIntervalTask(schedule, task);
            }
            else
            {
                var interval = new TimeSpan(schedule.ActionHour, schedule.ActionMinute, schedule.ActionSecond);
                var timeToGo = DateTime.Today + interval - DateTime.Now;
                if (timeToGo < TimeSpan.Zero)
                    timeToGo = timeToGo.Add(new TimeSpan(24, 0, 0));

                CreateScheduleTimer(schedule, interval, 0, task);
            }
        }

        /// <summary>
        /// Creates a per-n-second interval data logging action
        /// </summary>
        /// <param name="schedule">DataLogSchedule entity</param>
        /// <param name="task">Action task to perform</param>
        void CreatePerSecondIntervalTask(DataLogSchedule schedule, Action<DataLogSchedule> task)
        {
            var interval = TimeSpan.FromSeconds(schedule.ActionSecond);
            CreateScheduleTimer(schedule, interval, 0, task);
        }

        /// <summary>
        /// Creates a per-n-minute interval data logging action
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="task"></param>
        void CreatePerMinuteIntervalTask(DataLogSchedule schedule, Action<DataLogSchedule> task)
        {
            var interval = TimeSpan.FromMinutes(schedule.ActionMinute);
            CreateScheduleTimer(schedule, interval, 0, task);
        }

        /// <summary>
        /// Creates a per-n-hour interval data logging action
        /// with optional n-minute after the hour adjustment
        /// </summary>
        /// <param name="schedule"></param>
        /// <param name="task"></param>
        void CreatePerHourIntervalTask(DataLogSchedule schedule, Action<DataLogSchedule> task)
        {
            var interval = TimeSpan.FromHours(schedule.ActionHour);
            var delay = schedule.ActionMinute;
            CreateScheduleTimer(schedule, interval, delay, task);
        }

        void CreateScheduleTimer(DataLogSchedule schedule, TimeSpan interval, int delayMinutes, Action<DataLogSchedule> task)
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
