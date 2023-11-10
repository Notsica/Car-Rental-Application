using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
using Car_Rent.Data.Interfaces;
using Car_Rent.Common.Enums;

namespace Car_Rent.Business.Classes;

public class BookingProcessor
{
    readonly IData _data;
    public BookingProcessor(IData data) => _data = data;
    public InputVariables i = new();
    public UpdateVariables u = new();
   
    public List<Vehicle> GetVehiclesy() => _data.Get<Vehicle>();
    public List<IBooking> GetBookingsy() => _data.Get<IBooking>();
    public List<IPerson> GetPersonsy() => _data.Get<IPerson>();

    public void AddVehicle()
    {
        if (!string.IsNullOrEmpty(i.InputRegNo) &&
            !string.IsNullOrEmpty(i.InputMake) &&
            i.InputOdometer != null &&
            i.InputCostKm != null &&
            !string.IsNullOrEmpty(i.InputType))
        {
            int TypeCost = (int)GetVehicleType(i.InputType);

            Vehicle vehicle = i.InputType == "Motorcycle" ?
                new Motorcycle(_data.NextVehicleId, i.InputRegNo, i.InputMake, (int)i.InputOdometer, (int)i.InputCostKm, i.InputType, TypeCost, false) :
                new Car(_data.NextVehicleId, i.InputRegNo, i.InputMake, (int)i.InputOdometer, (int)i.InputCostKm, i.InputType, TypeCost, false);

            (i.InputRegNo, i.InputMake, i.InputOdometer, i.InputCostKm, i.InputType) = (string.Empty, string.Empty, null, null, null);
            _data.Add(vehicle);
        }
        else { u.NullCheck = true; }
    }
    public void AddPerson()
    {
        if (!string.IsNullOrEmpty(i.InputSSN) && !string.IsNullOrEmpty(i.InputFName) && !string.IsNullOrEmpty(i.InputLName))
        {
            var p = new Customer(i.InputSSN, i.InputFName, i.InputLName, _data.NextPersonId);
            _data.Add<IPerson>(p);
            (i.InputSSN, i.InputFName, i.InputLName) = (string.Empty, string.Empty, string.Empty);
        }
        else { u.NullCheck = true; }
    }

    public async Task RentVehicle(Vehicle vehicle)
    {
        if (i.PersonSelect != 0)
        {
            _data.RentVehicle(vehicle.Id, i.PersonSelect);
            i.PersonSelect = 0;

            u.FetchingData = true;
            await Task.Delay(2000);
            u.FetchingData = false;
        }
        else { u.NullCheck = true; }
    }
    public void ReturnVehicle(int vId)
    {
        if (i.InputKmRented != null)
        {
            _data.ReturnVehicle(vId, (double)i.InputKmRented);
        }
        else { u.NullCheck = true; }
    }

    public string[] VehicleTypeNames => _data.VehicleTypeNames;
    public VehicleTypes GetVehicleType(string name) => _data.GetVehicleType(name);
}