using System.ComponentModel.DataAnnotations;

namespace ShiftScheduling.Database;

public class User
{
    public string Email { get; set; }
    public UserType UserType { get; set; } 
    public string InternalId { get; set; }
    public User(string email, UserType userType, string internalId)
    {
        Email = email;
        UserType = userType;
        InternalId = internalId;
    }
}

public enum UserType : int
{
    Admin=1,
    User=2
}

public class UserDB
{
    [Key]
    public int UserId { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public int UserType { get; set; }
    public string InternalId { get; set; } = null!;

    public ICollection<ShiftDB> CreatedShifts { get; set; }
    public ICollection<AppliedUserDB> AppliedShifts { get; set; }
    public ICollection<ApprovedUserDB> ApprovedShifts { get; set; }
}
