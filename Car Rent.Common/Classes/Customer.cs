using Car_Rent.Common.Interfaces;
namespace Car_Rent.Common.Classes;

public class Customer : IPerson
{
    public string SSN { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int Id { get; init; }

    public Customer(string sSN, string firstName, string lastName, int uID) => 
        (SSN, FirstName, LastName, Id) = (sSN, firstName, lastName, uID);
}
