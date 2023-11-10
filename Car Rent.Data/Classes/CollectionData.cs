using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
using Car_Rent.Common.Enums;
using Car_Rent.Data.Interfaces;
using System.Linq.Expressions;
using Car_Rent.Common.Extensions;

namespace Car_Rent.Data.Classes;

public class CollectionData : IData
{
    readonly private List<Vehicle> _vehicles = new();
    readonly private List<IPerson> _persons = new();
    readonly private List<IBooking> _bookings = new();

    public CollectionData() => SeedData();
    void SeedData()
    {
        _vehicles.Add(new Car(NextVehicleId, "KYO123", "Volvo", 10000, 1, $"{VehicleTypes.Combi}", (int)VehicleTypes.Combi, false));
        _vehicles.Add(new Car(NextVehicleId, "MEN456", "Tesla", 4000, 1, $"{VehicleTypes.Sedan}", (int)VehicleTypes.Sedan, false));
        _vehicles.Add(new Car(NextVehicleId, "NON789", "Toyota", 20000, 3, $"{VehicleTypes.Sedan}", (int)VehicleTypes.Sedan, true));
        _vehicles.Add(new Motorcycle(NextVehicleId, "AMI012", "Yamaha", 2000, 1.5, $"{VehicleTypes.Motorcycle}", (int)VehicleTypes.Motorcycle, false));
        _vehicles.Add(new Car(NextVehicleId, "YUR420", "Jeep", 9000, 0.5, $"{VehicleTypes.Van}", (int)VehicleTypes.Van, false));

        _persons.Add(new Customer("13579", "Stevie", "Wonder", NextPersonId));
        _persons.Add(new Customer("31415", "Ada", "Lovelace", NextPersonId));

        _bookings.Add(new Booking(NextBookingId, "KYO123", $"{_persons[0].FirstName} {_persons[0].LastName}({_persons[0].SSN})", 10000, 10000, _vehicles[0].CostKm, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 200, _vehicles[0].Booked));
        _bookings.Add(new Booking(NextBookingId, "NON789", $"{_persons[1].FirstName} {_persons[1].LastName}({_persons[1].SSN})", 20000, 20000, _vehicles[2].CostKm, DateOnly.FromDateTime(DateTime.Now), DateOnly.FromDateTime(DateTime.Now), 150, _vehicles[2].Booked));
    }

    public List<T> Get<T>(Expression<Func<T, bool>>? expression = null) where T : class
    {
        try
        {
            return GetType().GetVariables().FindCollection<T>().GetData(this).ToQueryable<T>().Filter(expression);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public T Single<T>(Expression<Func<T, bool>>? expression = null) where T : class
    {
        try
        {
            return GetType().GetVariables().FindCollection<T>().GetData(this).ToQueryable<T>().Filter(expression).Single();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public void Add<T>(T item) where T : class
    {
        try
        {
            var collectionField = GetType()
                .GetVariables()
                .FindCollection<T>();

            collectionField.AddItem(this, item);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public int NextVehicleId => _vehicles.Count.Equals(0) ? 1 : _vehicles.Max(v => v.Id) + 1;
    public int NextPersonId => _persons.Count.Equals(0) ? 1 : _persons.Max(p => p.Id) + 1;
    public int NextBookingId => _bookings.Count.Equals(0) ? 1 : _bookings.Max(p => p.Id) + 1;

    public void RentVehicle(int vehicleId, int customerId)
    {
        var v = Single<Vehicle>(v => v.Id == vehicleId);
        var p = Single<IPerson>(p => p.Id == customerId);

        var booking = new Booking(NextBookingId, v.RegNo, $"{p.FirstName} {p.LastName}({p.SSN})", v.Odometer, v.Odometer, v.CostKm, TimeExtensions.TimeNow(),
            TimeExtensions.TimeNow(), v.CostDay, true);

        v.Booked = true;
        Add<IBooking>(booking);
    }
    public void ReturnVehicle(int vehicleId, double? plusKm)
    {
        plusKm ??= 0;
        var v = Single<Vehicle>(v =>  v.Id == vehicleId);
        var b = Single<IBooking>(b => b.RegNo == v.RegNo);
        int days = TimeExtensions.Duration(b.DateRented) + 1;

        b.KmReturned += (double)plusKm;
        v.Odometer = b.KmReturned;
        (v.Booked, b.Status) = (false, false);

        Booking bookingInstance = new(NextBookingId, b.RegNo, b.Customer, b.KmRented, b.KmReturned, b.CostKm, b.DateRented, b.DateReturned, b.TotalCost, b.Status);
        bookingInstance.CalcCost(v, days);
        b.TotalCost = bookingInstance.TotalCost;
    }
}