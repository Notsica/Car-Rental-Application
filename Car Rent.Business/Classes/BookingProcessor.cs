using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
using Car_Rent.Data.Interfaces;
using Car_Rent.Common.Extensions;
using Car_Rent.Common.Enums;

namespace Car_Rent.Business.Classes;

public class BookingProcessor
{
    readonly IData _data;
    public BookingProcessor(IData data) => _data = data;

    public InputVariables i = new InputVariables();
    public UpdateVariables u = new UpdateVariables();

    public List<Vehicle> GetVehicles() => _data.Get<Vehicle>();
    public List<IPerson> GetPersons() => _data.Get<IPerson>();
    public List<IBooking> GetBookings() => _data.Get<IBooking>();

    public void AddVehicle()
    {
        if (!string.IsNullOrEmpty(i.InputRegNo) &&
            !string.IsNullOrEmpty(i.InputMake) &&
            i.InputOdometer != null &&
            i.InputCostKm != null &&
            !string.IsNullOrEmpty(i.InputType))
        {
            int TypeCost = (int)GetVehicleType(i.InputType);
            Vehicle vehicle;

            vehicle = i.InputType == "Motorcycle" ?
                new Motorcycle(_data.NextVehicleId, i.InputRegNo, i.InputMake, (int)i.InputOdometer, (int)i.InputCostKm, i.InputType, TypeCost, false) :
                new Car(_data.NextVehicleId, i.InputRegNo, i.InputMake, (int)i.InputOdometer, (int)i.InputCostKm, i.InputType, TypeCost, false);

            (i.InputRegNo, i.InputMake, i.InputOdometer, i.InputCostKm, i.InputType) = (string.Empty, string.Empty, null, null, null);
            _data.Add(vehicle);
        }
        else { u.NullCheck = true; }
    }
    public void AddPerson()
    {
        if (!string.IsNullOrEmpty(i.SSN) && !string.IsNullOrEmpty(i.FName) && !string.IsNullOrEmpty(i.LName))
        {
            var p = new Customer(i.SSN, i.FName, i.LName, _data.NextPersonId);
            _data.Add<IPerson>(p);
            (i.SSN, i.FName, i.LName) = (string.Empty, string.Empty, string.Empty);
        }
        else { u.NullCheck = true; }
    }
    public async void RentVehicle(Vehicle vehicle)      
    {
        if (i.PersonSelect != 0)
        {
            _data.RentVehicle(vehicle.Id, i.PersonSelect);
            i.PersonSelect = 0;

            u.update = 1;
            u.FetchingData = true;
            await Task.Delay(2000);
            u.FetchingData = false;
        }
        else { u.NullCheck = true; }
    }
    public async Task Wait4() => await Task.Delay(3000);
    public void ReturnVehicle(int vId) => _data.ReturnVehicle(vId, (double)i.InputKmRented);

    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleTypes GetVehicleType(string name) => _data.GetVehicleType(name);
}