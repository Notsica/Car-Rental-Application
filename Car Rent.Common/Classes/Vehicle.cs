namespace Car_Rent.Common.Classes;

public abstract class Vehicle
{
    public int Id { get; init; }
    public string RegNo { get; init; }
    public string Make { get; init; }
    public double Odometer { get; set; }
    public double CostKm { get; init; }
    public string VehicleType { get; init; }
    public int CostDay { get; init; }
    public bool Booked { get; set; }
}
