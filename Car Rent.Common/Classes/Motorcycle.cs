using Car_Rent.Common.Interfaces;

namespace Car_Rent.Common.Classes;

public class Motorcycle : IVehicle
{
    public string RegNo { get; init; }
    public string Make { get; init; }
    public int Odometer { get; set; }
    public double CostKm { get; init; }
    public string VehicleType { get; init; } = "Motorcycle";
    public int CostDay { get; init; }
    public bool Booked { get; set; }

    public Motorcycle(string regNo, string make, int odometer, double costKm, string vehicletype, int costDay, bool status) =>
        (RegNo, Make, Odometer, CostKm, VehicleType, CostDay, Booked) = (regNo, make, odometer, costKm, vehicletype, costDay, status);
}
