using System.ComponentModel.DataAnnotations;

namespace ShiftScheduling.Database;

public class Shift
{
    public DateOnly CreationDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Place> Places { get; set; } = new List<Place>();
    public decimal PricePerUser { get; set; }
    public ShiftType ShiftType { get; set; }
    public User Creator { get; set; }
    public List<User> AppliedUsers { get; set; } = new List<User>();
    public List<User> ApprovedUsers { get; set; } = new List<User>();
    public int? MaxUsers { get; set; }
    public decimal TotalPrice { get; set; }
    public ShiftsStatus Status { get; set; } = ShiftsStatus.Open;

    public Shift(DateTime startDate, DateTime endDate, decimal pricePerUser, ShiftType shiftType, User creator, int? maxUsers = null)
    {
        StartDate = startDate;
        EndDate = endDate;
        PricePerUser = pricePerUser;
        ShiftType = shiftType;
        Creator = creator;
        MaxUsers = maxUsers;
    }
}
public enum ShiftsStatus : int
{
    Open = 0,
    Closed = 1
}


public class ShiftDB
{
    [Key]
    public int ShiftId { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerUser { get; set; }
    public int ShiftTypeId { get; set; }
    public ShiftTypeDB ShiftType { get; set; }
    public int CreatorUserId { get; set; }
    public UserDB CreatorUser { get; set; }
    public int? MaxUsers { get; set; }
    public decimal? TotalPrice { get; set; }
    public int Status { get; set; }

    public ICollection<ShiftPlaceDB> ShiftPlaces { get; set; }
    public ICollection<AppliedUserDB> AppliedUsers { get; set; }
    public ICollection<ApprovedUserDB> ApprovedUsers { get; set; }
}