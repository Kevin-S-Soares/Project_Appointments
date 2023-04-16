namespace Project_Appointments.Models
{
    public class TimeRepresentation
    {
        public static bool IsPartiallyInserted(
            ITimeRepresentation segment1, ITimeRepresentation segment2)
        {
            return
                GetStartTimeTicks(segment2) <= GetStartTimeTicks(segment1)
                && GetEndTimeTicks(segment2) >= GetStartTimeTicks(segment1)
                || GetStartTimeTicks(segment2) <= GetEndTimeTicks(segment1)
                && GetEndTimeTicks(segment2) >= GetEndTimeTicks(segment1);
        }

        public bool IsCompletelyInserted(
            ITimeRepresentation segment1, ITimeRepresentation segment2)
        {
            return
                GetStartTimeTicks(segment2) <= GetStartTimeTicks(segment1)
                && GetEndTimeTicks(segment2) >= GetStartTimeTicks(segment1)
                && GetStartTimeTicks(segment2) <= GetEndTimeTicks(segment1)
                && GetEndTimeTicks(segment2) >= GetEndTimeTicks(segment1);
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
