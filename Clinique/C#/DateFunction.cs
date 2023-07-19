namespace AspNetCoreTemplate.C_;

public class DateFunction
{
    public static string convertDate(DateOnly date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}