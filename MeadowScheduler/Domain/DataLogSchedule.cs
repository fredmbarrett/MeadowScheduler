using MeadowScheduler.Core.Enums;
using System.Text;

namespace MeadowScheduler.Domain
{
    internal class DataLogSchedule : ScheduleBase
    {
        public int ActionSecond { get; set; }

        /// <summary>
        /// ActionParam is a pointer to the data log action 
        /// or sensor to read for this scheduled event
        /// </summary>
        public int ActionParam { get; set; }

        public override string ToString()
        {
            string days = ((ActionDays & 0x01) == 0x01) ? "S" : "-";
            days += ((ActionDays & 0x02) == 0x02) ? "M" : "-";
            days += ((ActionDays & 0x04) == 0x04) ? "T" : "-";
            days += ((ActionDays & 0x08) == 0x08) ? "W" : "-";
            days += ((ActionDays & 0x10) == 0x10) ? "T" : "-";
            days += ((ActionDays & 0x20) == 0x20) ? "F" : "-";
            days += ((ActionDays & 0x40) == 0x40) ? "S" : "-";

            string time;
            if (ScheduleType == ScheduleType.RecurringEvent)
            {
                if (ActionSecond > 0) { time = $"Every {ActionSecond} seconds"; }
                else if (ActionHour > 0) { time = $"Every {ActionHour} at {ActionMinute} past the hour"; }
                else { time = $"Every {ActionMinute} minutes"; }
            }
            else
            {
                time = $"Occurs at {ActionHour:D2}:{ActionMinute:D2}:{ActionSecond:D2}";
            }

            string action = ActionType switch
            {
                0x01 => "Record BME688 Data",
                0x02 => "Record PMSA003i Data",
                0x03 => "Record Water Temperature",
                _ => "Error",
            };

            var sb = new StringBuilder()
                .Append($"Schedule - Id: {Id}, ")
                .Append($"Type: {ScheduleType}, ")
                .Append($"Days: {days}, ")
                .Append($"When: {time}, ")
                .Append($"Action: {action}, ")
                .Append($"Param: {ActionParam}");

            return sb.ToString();
        }
    }
}
