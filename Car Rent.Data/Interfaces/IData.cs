using Car_Rent.Common.Classes;
using Car_Rent.Common.Interfaces;
namespace Car_Rent.Data.Interfaces;

public interface IData
{
    List<IVehicle> GetVehicles();
    List<IPerson> GetPersons();
    List<IBooking> GetBookings();
}
