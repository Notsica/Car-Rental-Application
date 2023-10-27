using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
using Car_Rent.Data.Interfaces;

namespace Car_Rent.Business.Classes;

public class BookingProcessor
{
    readonly IData _data;
    public BookingProcessor(IData data) => _data = data;
    
    public string? PersonSelect { get; set; }
    public string InputRegNo { get; set; }
    public string InputMake { get; set; }
    public int? InputOdometer { get; set; }
    public int? InputCostKm { get; set; } 
    public string InputType { get; set; }
    public int? InputKmRented { get; set; }
    public string? InputFName { get; set; }
    public string? InputLName { get; set; }

    public List<IVehicle> GetVehicles() => _data.GetVehicles().GetRange(0, _data.GetVehicles().Count);
    public List<IPerson> GetPersons() => _data.GetPersons();
    public List<IBooking> GetBookings() => _data.GetBookings();

    public void AddVehicle()
    {
        if (InputType == "Motorcycle")
        {
            _data.GetVehicles().Add(new Car(InputRegNo, InputMake, (int)InputOdometer, (int)InputCostKm, InputType, 100, true));
            return;
        }
        GetVehicles().Add(new Car(InputRegNo, InputMake, (int)InputOdometer, (int)InputCostKm, InputType, 100, false)); //test
        _data.GetVehicles().Add(new Car(InputRegNo, InputMake, (int)InputOdometer, (int)InputCostKm, InputType, 100, false));
    }
    public void AddPerson(string inputFName, string inputLName)
    {
        GetPersons().Add(new Customer(inputFName, inputLName, GetPersons().Last().UID + 1));
    }
    public void RentVehicle(string regNo, int odometer, double costKm,double costDay)
    {
        GetBookings().Add(new Booking($"{regNo}", $"{PersonSelect}", odometer, odometer, costKm, DateOnly.FromDateTime(DateTime.Now),
        DateOnly.FromDateTime(DateTime.Now), costDay, true));
        _data.GetVehicles()[FindCar(regNo)].Booked = true;
    }
    public void ReturnVehicle(string vRegNo)
    {
        _data.GetBookings()[FindBooking(vRegNo)].KmReturned += (int)InputKmRented;
        _data.GetBookings()[FindBooking(vRegNo)].Status = false;
        _data.GetVehicles()[FindCar(vRegNo)].Odometer = (int)_data.GetBookings()[FindBooking(vRegNo)].KmReturned;
        _data.GetVehicles()[FindCar(vRegNo)].Booked = false;
    }

    public double GetTotalCost(double kmRented, double kmReturned,double costKm ,DateOnly dateRented, double costDay)
    {
        DateTime startDate = dateRented.ToDateTime(TimeOnly.MinValue);
        TimeSpan datePassed = startDate - DateTime.Now; 
        double totalCost = (kmReturned - kmRented) *costKm + datePassed.Days*costDay+costDay;
        return totalCost;
    }
    int FindCar(string targetRegNo)
    {
        int index = _data.GetVehicles().FindIndex(car => car.RegNo == targetRegNo);
        return index;
    }
    int FindBooking(string targetRegNo)
    {
        var matchingBooking = _data.GetBookings()
        .Select((booking, index) => new { Booking = booking, Index = index })
        .Where(item => item.Booking.RegNo == targetRegNo)
        .ToList();

        int lastIndex = matchingBooking.Last().Index;
        return lastIndex;
    }
}