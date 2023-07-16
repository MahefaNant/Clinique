namespace AspNetCoreTemplate.C_;

public class DateTimeToUTC
{
    public static DateTime Make(DateTime dateTime)
    {
        dateTime =DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        dateTime = dateTime.ToUniversalTime();
        return dateTime;
    }
}