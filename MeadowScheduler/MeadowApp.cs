using Meadow;
using Meadow.Devices;
using Meadow.Logging;
using MeadowScheduler.Controllers;
using MeadowScheduler.Core;
using MeadowScheduler.Core.Enums;
using MeadowScheduler.Data;
using MeadowScheduler.Domain;
using MeadowScheduler.Schedulers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static MeadowScheduler.Domain.CallInSchedule;

namespace MeadowScheduler
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
    {
        protected static Logger Log { get => Resolver.Log; }

        SchedulerSettings _appSettings;
        SchedulesController _schedules;
        CallInScheduler _callInScheduler;
        DataLogScheduler _dataLogScheduler;

        public override Task Initialize()
        {
            Log.Loglevel = LogLevel.Trace;
            Resolver.Log.Info("MeadowScheduler initialize...");

            // get apps settings and put into app resolver storage
            _appSettings = new SchedulerSettings();
            Resolver.Services.Add(_appSettings, typeof(SchedulerSettings));

            InitializeScheduleController();
            InitializeCallInSchedules();
            InitializeDataLogSchedules();

            return base.Initialize();
        }

        public override Task Run()
        {
            Resolver.Log.Info("MeadowScheduler run...");
            return base.Run();
        }

        void InitializeScheduleController()
        {
            var dbContext = new InMemoryDataContext();

            _schedules = SchedulesController.Instance;
            _schedules.Initialize(dbContext);
        }

        void InitializeCallInSchedules(List<CallInSchedule> callSchedules = null)
        {
            Log.Debug("Initializing Device Call-In Schedules...");

            _callInScheduler = CallInScheduler.Instance;
            _callInScheduler.ClearTimers();

            callSchedules ??= _schedules.GetAllCallInSchedules();

            foreach (var call in callSchedules)
            {
                Log.Debug($"...creating call-in schedule: {call}");
                _callInScheduler.CreateCallInTask(call, (call) =>
                {
                    var action = (CallInAction)call.ActionType;
                    var actionName = Enum.GetName(typeof(CallInAction), action);
                    var actionHour = call.ActionHour;
                    var actionMinute = call.ActionMinute;

                    Log.Debug($"Executing {actionName} call for time {actionHour:D2}:{actionMinute:D2}...");

                    switch (action)
                    {
                        case CallInAction.REPORT_STATUS:
                            Log.Debug("...queueing SEND_DEVICE_STATUS command...");
                            // TODO: Create code to execute device status call in
                            break;
                        case CallInAction.REPORT_DATA:
                            Log.Debug("...queueing SEND_SENSOR_REPORT command...");
                            // TODO: Create code to execute device data report
                            break;
                        default:
                            break;
                    }

                    Log.Debug($"Execution of {actionName} complete.");
                });
            }
        }

        /// <summary>
        /// Initialize the data log schedules from either the 
        /// object list passed, or the previously-stored database items
        /// </summary>
        /// <param name="schedules"></param>
        void InitializeDataLogSchedules(List<DataLogSchedule> dataLogSchedules = null)
        {
            Log.Debug("Initializing Sensor Data Log Schedules...");

            _dataLogScheduler = DataLogScheduler.Instance;
            _dataLogScheduler.ClearTimers();

            dataLogSchedules ??= _schedules.GetActiveDataLogSchedules();

            foreach (var datalog in dataLogSchedules)
            {
                var actionType = datalog.ActionType;
                var actionHour = datalog.ActionHour;
                var actionMinute = datalog.ActionMinute;
                var actionSecond = datalog.ActionSecond;

                Log.Debug($"...creating data log schedule: {datalog}");
                _dataLogScheduler.CreateDataLogTask(datalog, async (datalog) =>
                {
                    Log.Debug($"Executing data log action {actionType} for time {actionHour:D2}:{actionMinute:D2}:{actionSecond:D2}...");
                    // TODO: Create code to take sensor readings and log data

                    Log.Debug($"Data log execution complete: {datalog}");

                    // Meadow GC cleanup (may not be needed for Meadow versions 1.0 or later)
                    GC.Collect();
                });
            }
        }

    }
}