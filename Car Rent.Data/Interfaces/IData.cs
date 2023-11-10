using Car_Rent.Common.Enums;
using System.Linq.Expressions;
namespace Car_Rent.Data.Interfaces;

public interface IData
{
    public List<T> Get<T>(Expression<Func<T, bool>>? expression = null) where T : class;
    public T Single<T>(Expression<Func<T, bool>>? expression = null) where T : class;
    public void Add<T>(T item) where T : class;

    int NextVehicleId { get; }
    int NextPersonId { get; }
    int NextBookingId { get; }
    void RentVehicle(int vehicleId, int customerId);
    void ReturnVehicle(int vehicleId, double? plusKm);
    public string[] VehicleTypeNames => Enum.GetNames(typeof(VehicleTypes));
    public VehicleTypes GetVehicleType(string name) => Enum.Parse<VehicleTypes>(name);
}
