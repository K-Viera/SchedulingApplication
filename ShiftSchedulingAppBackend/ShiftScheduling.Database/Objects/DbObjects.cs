namespace ShiftScheduling.Database;

public class ShiftPlaceDB
{
    public int ShiftId { get; set; }
    public ShiftDB Shift { get; set; }
    public int PlaceId { get; set; }
    public PlaceDB Place { get; set; }
}

public class AppliedUserDB
{
    public int ShiftId { get; set; }
    public ShiftDB Shift { get; set; }
    public int UserId { get; set; }
    public UserDB User { get; set; }
}

public class ApprovedUserDB
{
    public int ShiftId { get; set; }
    public ShiftDB Shift { get; set; }
    public int UserId { get; set; }
    public UserDB User { get; set; }
}