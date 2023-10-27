using Car_Rent.Common.Interfaces;
using System.Text.RegularExpressions;

namespace Car_Rent.Common.Classes;

public class Booking : IBooking
{
    public string RegNo { get; init; }
    public string Customer { get; init; }
    public double KmRented { get; init; }
    public double KmReturned { get; set; }
    public double CostKm { get; init; }
    public DateOnly DateRented { get; init; }
    public DateOnly DateReturned { get; init; }
    public double Cost { get; set; }
    public bool Status { get; set; }
    public Booking(string regNo, string customer, double kmRented, double kmReturned, double costKm, DateOnly dateRented, DateOnly dateReturned, double cost, bool status)
    {
        RegNo = regNo;
        Customer = customer;
        KmRented = kmRented;
        KmReturned = kmReturned;
        CostKm = costKm; 
        DateRented = dateRented;
        DateReturned = dateReturned;
        Cost = cost;
        Status =status;
    }
    public void CalcCost(IVehicle vehicle, int days)
    {
        Cost = days * vehicle.CostDay + KmReturned * vehicle.CostKm;
        
    }
}
