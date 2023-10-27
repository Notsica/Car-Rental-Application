using Car_Rent.Common.Interfaces;

namespace Car_Rent.Common.Classes;

public class Customer : IPerson
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public int UID { get; init; }

    public Customer(string firstName, string lastName, int uID) => 
        (FirstName, LastName, UID) = (firstName, lastName, uID);
    
}
