namespace Car_Rent.Common.Interfaces;

public interface IPerson
{
    string SSN { get; init; }
    string FirstName { get; init; }
    string LastName { get; init; }
    int Id { get; init; }
}
