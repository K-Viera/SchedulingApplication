using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ShiftScheduling.Database;

public interface IPlaceRepository
{
    Task<int> AddPlace(Place place);
    Task<bool> DeletePlace(string name);
    Task<bool> DeleteById(int id);
    Task<IEnumerable<PlaceResponse>> GetAll();
    Task<Place?> GetPlace(int id);
    Task<bool> UpdatePlace(int placeId, Place place);
}

public class PlaceRepository : IPlaceRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public PlaceRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<PlaceResponse>> GetAll()
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var places = await context.Places.ToListAsync();
        return places.Select(p => p.ToPlaceResponse());
    }

    public async Task<Place?> GetPlace(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var place = await context.Places.FindAsync(id);
        return place?.ToPlace();
    }

    public async Task<bool> UpdatePlace(int placeId, Place place)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var placeDB = await context.Places.FindAsync(placeId);
        if (placeDB == null)
        {
            return false;
        }
        placeDB.Name = place.Name;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> AddPlace(Place place)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var placeDB = new PlaceDB
        {
            Name = place.Name
        };
        context.Places.Add(placeDB);
        await context.SaveChangesAsync();
        return placeDB.PlaceId;
    }

    public async Task<bool> DeletePlace(string name)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var placeDB = await context.Places.FirstOrDefaultAsync(p => p.Name == name);
        if (placeDB == null)
        {
            return false;
        }
        context.Places.Remove(placeDB);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(int id)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();
        var placeDB = await context.Places.FindAsync(id);
        if (placeDB == null)
        {
            return false;
        }
        context.Places.Remove(placeDB);
        int result = await context.SaveChangesAsync();
        return result > 0;

    }
}
