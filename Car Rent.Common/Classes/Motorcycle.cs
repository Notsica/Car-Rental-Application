namespace Car_Rent.Common.Classes;

public class Motorcycle : Vehicle
{
    public Motorcycle(int id, string regNo, string make, double odometer, double costKm, string vehicletype, int costDay, bool status) =>
        (Id, RegNo, Make, Odometer, CostKm, VehicleType, CostDay, Booked) = (id, regNo, make, odometer, costKm, vehicletype, costDay, status);
}
