namespace Car_Rent.Common.Extensions;

public static class TimeExtensions
{
    public static int Duration(this DateOnly startDate)
    {
        DateTime endDate = DateTime.Now;
        TimeSpan duration = startDate.ToDateTime(TimeOnly.MinValue) - endDate;
        return duration.Days;
    }
    public static DateOnly TimeNow()
    {
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        return currentDate;
    }
}
