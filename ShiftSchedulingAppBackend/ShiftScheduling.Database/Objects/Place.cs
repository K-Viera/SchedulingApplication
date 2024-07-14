using System.ComponentModel.DataAnnotations;

namespace ShiftScheduling.Database;

public class Place
{
    public string Name { get; set; }
}

public class PlaceResponse: Place
{
    public int PlaceId { get; set; }
}
public class PlaceDB
{
    [Key]
    public int PlaceId { get; set; }
    public string Name { get; set; }

    public ICollection<ShiftPlaceDB> ShiftPlaces { get; set; }

    public Place ToPlace()
    {
        return new Place
        {
            Name = Name
        };
    }

    public PlaceResponse ToPlaceResponse()
    {
        return new PlaceResponse
        {
            PlaceId = PlaceId,
            Name = Name
        };
    }
}

