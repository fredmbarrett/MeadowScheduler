using MeadowScheduler.Domain;
using System.Collections.Generic;

namespace MeadowScheduler.Data
{
    internal interface IDataContext
    {
        /// <summary>
        /// Next message number to be sent by device
        /// </summary>
        int NextDeviceMessageId { get; }

        /// <summary>
        /// Schedule entries for when the Envizor device is to call the server
        /// </summary>
        List<CallInSchedule> CallInSchedules { get; set; }

        /// <summary>
        /// Schedule entries for when the Envizor device is to take sensor readings
        /// </summary>
        List<DataLogSchedule> DataLogSchedules { get; set; }

    }
}
