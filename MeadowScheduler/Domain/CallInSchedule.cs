using MeadowScheduler.Core.Enums;
using System.Text;

namespace MeadowScheduler.Domain
{
    internal class CallInSchedule : ScheduleBase
    {
        public CallInSchedule() { }

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
                if (ActionHour > 0) { time = $"Every {ActionHour} at {ActionMinute} past the hour"; }
                else { time = $"Every {ActionMinute} minutes"; }
            }
            else
            {
                time = $"Occurs at {ActionHour:D2}:{ActionMinute:D2}";
            }

            string action = ActionType switch
            {
                0x01 => "Report Status",
                0x02 => "Report Data",
                0x03 => "Cell Standby",
                0x04 => "Cell Off",
                _ => "Error",
            };

            var sb = new StringBuilder()
                .Append($"Id: {Id}; ")
                .Append($"Type: {ScheduleType}; ")
                .Append($"Days: {days}; ")
                .Append($"When: {time}; ")
                .Append($"Action: {action}");

            return sb.ToString();
        }
    }
}
