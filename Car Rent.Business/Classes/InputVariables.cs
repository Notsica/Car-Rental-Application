namespace Car_Rent.Business.Classes;

public class InputVariables
{
    #region Vehicle Inputs
    public string InputRegNo { get; set; }
    public string InputMake { get; set; }
    public int? InputOdometer { get; set; }
    public int? InputCostKm { get; set; }
    public string? InputType { get; set; }
    public double? InputKmRented { get; set; }
    public int PersonSelect { get; set; }
    #endregion

    #region Customer Inputs
    public string? SSN { get; set; }
    public string? FName {get; set; }
    public string? LName { get; set; }
    #endregion
}

