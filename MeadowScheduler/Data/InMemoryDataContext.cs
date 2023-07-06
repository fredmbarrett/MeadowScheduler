using MeadowScheduler.Domain;
using System.Collections.Generic;
using MeadowScheduler.Core.Enums;

namespace MeadowScheduler.Data
{
    internal class InMemoryDataContext : DataContextBase, IDataContext
    {
        #region IDataContext Properties / Methods

        /// <summary>
        /// List of call-in schedules known to the device
        /// </summary>
        public List<CallInSchedule> CallInSchedules
        {
            get => _callInSchedules ?? new List<CallInSchedule>();
            set
            {
                var id = 1;
                _callInSchedules = new List<CallInSchedule>();
                value.ForEach(p =>
                {
                    p.Id = id++;
                    _callInSchedules.Add(p);
                });
            }
        }
        List<CallInSchedule> _callInSchedules;

        /// <summary>
        /// List of data logging schedules known to the device
        /// </summary>
        public List<DataLogSchedule> DataLogSchedules
        {
            get => _dataLogSchedules ?? new List<DataLogSchedule>();
            set
            {
                // simulate database identity column
                var id = 1;
                _dataLogSchedules = new List<DataLogSchedule>();
                value.ForEach(p =>
                {
                    p.Id = id++;
                    _dataLogSchedules.Add(p);
                });
            }
        }
        List<DataLogSchedule> _dataLogSchedules;


        #endregion IDataContext Properties / Methods


        public InMemoryDataContext()
        {
            Log.Info("InMemoryDataContext ctor called");
            Initialize();
        }

        void Initialize()
        {
            // Initialize CallInSchedules and add sample data
            CallInSchedules = new List<CallInSchedule>
            {
                new CallInSchedule { Id = 1, ScheduleType = ScheduleType.RecurringEvent, ActionDays = 0x7F, ActionHour = 1, ActionMinute = 5, ActionType = 1, IsActive = true },
                new CallInSchedule { Id = 2, ScheduleType = ScheduleType.RecurringEvent, ActionDays = 0x7F, ActionHour = 4, ActionMinute = 0, ActionType = 2, IsActive = true }
            };

            // Initialize DataLogSchedules and add sample data
            DataLogSchedules = new List<DataLogSchedule>
            {
                new DataLogSchedule { Id = 1, ScheduleType= ScheduleType.RecurringEvent, ActionDays = 0x7F, ActionHour = 0, ActionMinute = 0, ActionSecond = 15, ActionType = 1, IsActive = true },
                new DataLogSchedule { Id = 2, ScheduleType = ScheduleType.RecurringEvent, ActionDays = 0x7F, ActionHour = 0, ActionMinute = 1, ActionSecond = 0, ActionType = 2, IsActive = true }
            };
        }
    }
}
