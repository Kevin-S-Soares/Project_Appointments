namespace Project_Appointments.Models
{
    public class TimeRepresentation
    {
        public static bool IsPartiallyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            return
                GetStartTimeTicks(contains) <= GetStartTimeTicks(contained)
                && GetEndTimeTicks(contains) >= GetStartTimeTicks(contained)
                || GetStartTimeTicks(contains) <= GetEndTimeTicks(contained)
                && GetEndTimeTicks(contains) >= GetEndTimeTicks(contained);
        }

        public static bool IsCompletelyInserted(
            ITimeRepresentation contained, ITimeRepresentation contains)
        {
            return
                GetStartTimeTicks(contains) <= GetStartTimeTicks(contained)
                && GetEndTimeTicks(contains) >= GetStartTimeTicks(contained)
                && GetStartTimeTicks(contains) <= GetEndTimeTicks(contained)
                && GetEndTimeTicks(contains) >= GetEndTimeTicks(contained);
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
