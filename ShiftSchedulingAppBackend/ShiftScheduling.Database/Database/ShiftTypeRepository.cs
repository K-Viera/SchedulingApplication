using Microsoft.EntityFrameworkCore;

namespace ShiftScheduling.Database;


public interface IShiftTypeRepository
{
    Task<int> AddShiftType(ShiftType shiftType);
    Task<bool> DeleteShiftType(string name);
    Task<bool> DeleteById(int id);
    Task<IEnumerable<ShiftTypeResponse>> GetAll();
    Task<ShiftTypeResponse> GetShiftType(int id);
    Task<bool> UpdateShiftType(int shiftTypeId, ShiftType shiftType);
}
public class ShiftTypeRepository : IShiftTypeRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public ShiftTypeRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<ShiftTypeResponse>> GetAll()
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypes = await context.ShiftTypes.ToListAsync();
        return shiftTypes.Select(st => new ShiftTypeResponse(st.Name, st.Description, (double)st.Price)
        {
            NightPrice = (double?)st.NightPrice,
            HolidayPrice = (double?)st.HolidayPrice,
            ShiftTypeId = st.ShiftTypeId
        });
    }

    public async Task<ShiftTypeResponse> GetShiftType(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypeDB = await context.ShiftTypes.FindAsync(id);
        if (shiftTypeDB == null) return null;
        return new ShiftTypeResponse(shiftTypeDB.Name, shiftTypeDB.Description, (double)shiftTypeDB.Price)
        {
            NightPrice = (double?)shiftTypeDB.NightPrice,
            HolidayPrice = (double?)shiftTypeDB.HolidayPrice,
            ShiftTypeId = shiftTypeDB.ShiftTypeId
        };
    }

    public async Task<bool> UpdateShiftType(int shiftTypeId, ShiftType shiftType)
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypeDB = await context.ShiftTypes.FindAsync(shiftTypeId);
        if (shiftTypeDB == null)
        {
            return false;
        }
        shiftTypeDB.Name = shiftType.Name;
        shiftTypeDB.Description = shiftType.Description;
        shiftTypeDB.Price = (decimal)shiftType.Price;
        shiftTypeDB.NightPrice = (decimal?)shiftType.NightPrice;
        shiftTypeDB.HolidayPrice = (decimal?)shiftType.HolidayPrice;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<int> AddShiftType(ShiftType shiftType)
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypeDB = new ShiftTypeDB
        {
            Name = shiftType.Name,
            Description = shiftType.Description,
            Price = (decimal)shiftType.Price,
            NightPrice = (decimal?)shiftType.NightPrice,
            HolidayPrice = (decimal?)shiftType.HolidayPrice
        };
        context.ShiftTypes.Add(shiftTypeDB);
        await context.SaveChangesAsync();
        return shiftTypeDB.ShiftTypeId;
    }

    public async Task<bool> DeleteShiftType(string name)
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypeDB = await context.ShiftTypes.FirstOrDefaultAsync(st => st.Name == name);
        if (shiftTypeDB == null)
        {
            return false;
        }
        context.ShiftTypes.Remove(shiftTypeDB);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteById(int id)
    {
        using var context = _contextFactory.CreateDbContext();
        var shiftTypeDB = context.ShiftTypes.Find(id);
        if (shiftTypeDB == null)
        {
            return false;
        }
        context.ShiftTypes.Remove(shiftTypeDB);
        int result = await context.SaveChangesAsync();
        return result > 0;
    }
}
