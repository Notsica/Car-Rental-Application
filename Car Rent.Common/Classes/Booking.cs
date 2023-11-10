using Car_Rent.Common.Interfaces;
namespace Car_Rent.Common.Classes;

public class Booking : IBooking
{
    public int Id { get; init; }
    public string RegNo { get; init; }
    public string Customer { get; init; }
    public double KmRented { get; init; }
    public double KmReturned { get; set; }
    public double CostKm { get; init; }
    public DateOnly DateRented { get; init; }
    public DateOnly DateReturned { get; init; }
    public double TotalCost { get; set; }
    public bool Status { get; set; }
    public Booking(int id, string regNo, string customer, double kmRented, double kmReturned, double costKm, 
        DateOnly dateRented, DateOnly dateReturned, double totalCost, bool status)
    {
        Id = id;
        RegNo = regNo;
        Customer = customer;
        KmRented = kmRented;
        KmReturned = kmReturned;
        CostKm = costKm; 
        DateRented = dateRented;
        DateReturned = dateReturned;
        TotalCost = totalCost;
        Status =status;
    }
    public void CalcCost(Vehicle vehicle, int days)
    {
        TotalCost = days * vehicle.CostDay + (KmReturned-KmRented) * vehicle.CostKm;
    }
}
