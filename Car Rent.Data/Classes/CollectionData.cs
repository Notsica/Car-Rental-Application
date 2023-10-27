using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
using Car_Rent.Common.Enums;
using Car_Rent.Data.Interfaces;

namespace Car_Rent.Data.Classes;

public class CollectionData : IData
{
    readonly public List<IVehicle> _vehicles = new();
    readonly public List<IPerson> _persons = new();
    readonly public List<IBooking> _bookings = new();

    public CollectionData() => SeedData();

    void SeedData()
    {
        _vehicles.Add(new Car("KYO123", "Volvo", 10000, 1, $"{VehicleTypes.Combi}", 200, false));
        _vehicles.Add(new Car("MEN456", "Tesla", 4000, 1, $"{VehicleTypes.Sedan}", 100, false));
        _vehicles.Add(new Car("NON789", "Toyota", 20000, 3, $"{VehicleTypes.Sedan}", 150, true));
        _vehicles.Add(new Car("AMI012", "Yamaha", 2000, 1.5, $"{VehicleTypes.Motorcycle}", 300, false));
        _vehicles.Add(new Car("YUR420", "Jeep", 9000, 0.5, $"{VehicleTypes.Van}", 70, false));

        _persons.Add(new Customer("John", "Smith", 1000));
        _persons.Add(new Customer("Sekerie", "Ahmed", 1001));

        _bookings.Add(new Booking("KYO123", "John Smith(1000)", 10000, 10000, _vehicles[0].CostKm, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 200, _vehicles[0].Booked));
        _bookings.Add(new Booking("NON789", "Sekerie Ahmed(1001)", 20000, 20000, _vehicles[2].CostKm, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 150, _vehicles[2].Booked));
    }

    public List<IVehicle> GetVehicles() => _vehicles;
    public List<IPerson> GetPersons() => _persons;
    public List<IBooking> GetBookings() => _bookings;
}