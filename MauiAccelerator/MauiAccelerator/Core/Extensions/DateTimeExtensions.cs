using System;

namespace MauiAccelerator.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTime ToUtcFromUnixTimestamp(this string? unixTimestampValue)
    {
        if (!double.TryParse(unixTimestampValue, out double unixTimestamp))
        {
            return DateTime.MinValue;
        }
        
        // Unix timestamp is seconds past epoch
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimestamp).ToUniversalTime();
        return dateTime;
    }
}