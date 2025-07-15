namespace Local_WRP.Helpers
{
    public class TimeZonesHelper
    {
        /// <summary>
        /// Returns the current local time for the specified time zone ID, based on UTC.
        /// </summary>
        /// <param name="timeZoneId">The IANA or Windows time zone ID.</param>
        /// <returns>The current local DateTime in the specified time zone, or null if the ID is invalid.</returns>
        public static DateTime? GetCurrentLocalTime(string timeZoneId, DateTime? InputDate)
        {
            if (string.IsNullOrWhiteSpace(timeZoneId))
                return null;

            try
            {
                var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return TimeZoneInfo.ConvertTimeFromUtc(InputDate.HasValue ? InputDate.Value : DateTime.UtcNow, tz);
            }
            catch (TimeZoneNotFoundException)
            {
                return null;
            }
            catch (InvalidTimeZoneException)
            {
                return null;
            }
        }
    }
}
