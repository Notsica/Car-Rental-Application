namespace Car_Rent.Common.Extensions;

public static class VehicleExtension
{
    public static int Duration(this DateOnly startDate)
    {
        DateTime endDate = DateTime.Now;
        TimeSpan duration = startDate.ToDateTime(TimeOnly.MinValue) - endDate;
        return duration.Days;
    }
}
