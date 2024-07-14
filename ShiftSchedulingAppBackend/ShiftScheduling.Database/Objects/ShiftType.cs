using System.ComponentModel.DataAnnotations;

namespace ShiftScheduling.Database;

public class ShiftType
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public double? NightPrice { get; set; }
    public double? HolidayPrice { get; set; }
    public ShiftType(string name, string description, double price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}

public class ShiftTypeResponse : ShiftType
{
    public ShiftTypeResponse(string name, string description, double price) : base(name, description, price)
    {
    }
    public int? ShiftTypeId { get; set; }
}

public class ShiftTypeDB
{
    [Key]
    public int ShiftTypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal? NightPrice { get; set; }
    public decimal? HolidayPrice { get; set; }
    public ICollection<ShiftDB> Shifts { get; set; }
}
