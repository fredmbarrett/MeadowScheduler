using MeadowScheduler.Data;
using MeadowScheduler.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MeadowScheduler.Controllers
{
    internal class SchedulesController
    {
        #region Singleton Declaration

        private static readonly Lazy<SchedulesController> instance =
            new Lazy<SchedulesController>(() => new SchedulesController());

        public static SchedulesController Instance => instance.Value;

        private SchedulesController() : base() { }

        #endregion Singleton Declaration

        private bool _isInitialized = false;

        /// <summary>
        /// Initialize SchedulesController with IDataContext
        /// </summary>
        /// <param name="data"></param>
        public void Initialize(IDataContext data)
        {
            _data = data ??
                throw new ArgumentNullException(nameof(data));
            _isInitialized = true;
        }

        /// <summary>
        /// Last saved set of data logging schedules
        /// </summary>
        public List<DataLogSchedule> CurrentDataLogSchedules { get; private set; }

        private IDataContext _data;

        /// <summary>
        /// Gets all call in schedules known to the device
        /// </summary>
        /// <returns></returns>
        public List<CallInSchedule> GetAllCallInSchedules()
        {
            if (!_isInitialized) throw new InvalidOperationException("ScheduleController is not initialized");

            return _data.CallInSchedules
                .Where(p => p.IsActive == true)
                .ToList();
        }

        /// <summary>
        /// Gets all data log schedules known to the device
        /// </summary>
        /// <returns></returns>
        public List<DataLogSchedule> GetActiveDataLogSchedules()
        {
            if (!_isInitialized) throw new InvalidOperationException("ScheduleController is not initialized");

            return _data.DataLogSchedules
                .Where(p => p.IsActive == true)
                .ToList();
        }
    }
}
