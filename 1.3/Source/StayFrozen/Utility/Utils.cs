using System.Text;
using UnityEngine;
using Verse;

namespace StayFrozen.Utility
{
    public static class Utils
    {
        private const int TICKS_PER_INGAME_HOUR = 2500;
        private const int INGAME_HOURS_PER_INGAME_DAY = 24;

        public static string ToStringDuration(int ticks)
        {
            StringBuilder stringBuilder = new StringBuilder();
            int hours = TicksToHours(ticks);
            int days = HoursToDays(ref hours);
            stringBuilder.Append("DurationAsString".Translate(days, hours));
            return stringBuilder.ToString();
        }

        public static int TicksToHours(int ticks)
        {
            int hours = Mathf.RoundToInt(ticks / (float)TICKS_PER_INGAME_HOUR);
            ticks -= hours * TICKS_PER_INGAME_HOUR;
            return hours;
        }

        public static int HoursToDays(ref int hours)
        {
            int days = hours / INGAME_HOURS_PER_INGAME_DAY;
            hours -= days * INGAME_HOURS_PER_INGAME_DAY;
            return days;
        }
    }
}
