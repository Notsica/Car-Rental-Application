namespace Car_Rent.Common.Interfaces;

public interface IVehicle
{
    string RegNo { get; init; }
    string Make { get; init; }
    int Odometer { get; set; }
    double CostKm { get; init; }
    string VehicleType { get; init; }
    int CostDay { get; init; }
    bool Booked { get; set; }
}
