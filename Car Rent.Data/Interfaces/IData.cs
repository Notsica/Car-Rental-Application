using Car_Rent.Common.Enums;
using System.Linq.Expressions;
namespace Car_Rent.Data.Interfaces;

public interface IData
{
    List<T> Get<T>(Expression<Func<T, bool>>? expression = null);
    T? Single<T>(Expression<Func<T, bool>>? expression);
    public void Add<T>(T item);
    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    void RentVehicle(int vehicleId, int customerId);
    void ReturnVehicle(int vehicleId, double? plusKm);
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => Enum.Parse<VehicleTypes>(name);
}
