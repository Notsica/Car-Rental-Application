namespace Car_Rent.Common.Interfaces;

public interface IBooking
{
    string RegNo { get; init; }
    string Customer { get; init; }
    double KmRented { get; init; }
    double KmReturned { get; set; }
    double CostKm { get; init; }
    DateOnly DateRented { get; init; }
    DateOnly DateReturned { get; init; }
    double Cost {  get; set; }
    bool Status { get; set; }
}
