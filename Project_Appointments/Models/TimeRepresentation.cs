﻿namespace Project_Appointments.Models
{
    public class TimeRepresentation
    {
        public static bool IsPartiallyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = GetStartTimeTicks(contains);
            endTicksContains = GetEndTimeTicks(contains);
            startTicksContained = GetStartTimeTicks(contained);
            endTicksContained = GetEndTimeTicks(contained);

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 || condition2;
        }

        public static bool IsCompletelyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            long startTicksContains, endTicksContains, startTicksContained, endTicksContained;
            startTicksContains = GetStartTimeTicks(contains);
            endTicksContains = GetEndTimeTicks(contains);
            startTicksContained = GetStartTimeTicks(contained);
            endTicksContained = GetEndTimeTicks(contained);

            bool condition1 = startTicksContains <= startTicksContained
                && endTicksContains >= startTicksContained;

            bool condition2 = startTicksContains <= endTicksContained
                && endTicksContains >= endTicksContained;

            return condition1 && condition2;
        }

        private static long GetStartTimeTicks(ITimeRepresentation obj)
        {
            return Convert.ToInt64(obj.StartDay) * TimeSpan.TicksPerDay + obj.StartTime.Ticks;
        }

        private static long GetEndTimeTicks(ITimeRepresentation obj)
        {
            return Convert.ToInt64(obj.EndDay) * TimeSpan.TicksPerDay + obj.EndTime.Ticks;
        }
    }
}
