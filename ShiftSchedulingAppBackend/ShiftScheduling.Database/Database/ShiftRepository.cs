using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftScheduling.Database;

public interface IShiftRepository
{
    Task<int> AddShift(ShiftRequest shift);
    Task<bool> DeleteShiftById(int id);
    Task<IEnumerable<ShiftResponse>> GetAll();
    Task<ShiftResponse> GetShift(int id);
}

public class ShiftRepository : IShiftRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public ShiftRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<int> AddShift(ShiftRequest shift)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        //TODO calcule price
        decimal price = 0;
        //TODO set creator user id
        int creatorUserId = 1;
        ShiftDB shiftDB = new ShiftDB
        {
            CreationDate = DateTime.Now,
            StartDate = shift.StartDate,
            EndDate = shift.EndDate,
            PricePerUser = price,
            CreatorUserId = creatorUserId,
            MaxUsers = shift.MaxUsers,
            TotalPrice = shift.TotalPrice,
            Status = (int)ShiftsStatus.Open,
            ShiftTypeId = shift.ShiftTypeId,
        };
        context.Shifts.Add(shiftDB);
        await context.SaveChangesAsync();

        ShiftPlaceDB shiftPlaceDB = new ShiftPlaceDB
        {
            ShiftId = shiftDB.ShiftId,
            PlaceId = shift.PlaceId
        };
        context.ShiftPlaces.Add(shiftPlaceDB);
        await context.SaveChangesAsync();
        return shiftDB.ShiftId;
    }

    public async Task<bool> DeleteShiftById(int id)
    {
        using var context =await _contextFactory.CreateDbContextAsync();
        var shift = await context.Shifts.FindAsync(id);
        if (shift == null)
        {
            return false;
        }
        context.Shifts.Remove(shift);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<ShiftResponse>> GetAll()
    {
        using var context =await _contextFactory.CreateDbContextAsync();
        var shifts = await context.Shifts.ToListAsync();
        return shifts.Select(s => new ShiftResponse
        {
            ShiftId = s.ShiftId,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            PricePerUser = s.PricePerUser,
            ShiftTypeId = s.ShiftTypeId,
            CreatorUserId = s.CreatorUserId,
            MaxUsers = s.MaxUsers,
            TotalPrice = s.TotalPrice,
            Status = s.Status
        });
    }

    public async Task<ShiftResponse> GetShift(int id)
    {
        using var context =await _contextFactory.CreateDbContextAsync();
        var shift = await context.Shifts.FindAsync(id);
        if (shift == null) return null;
        return new ShiftResponse
        {
            ShiftId = shift.ShiftId,
            StartDate = shift.StartDate,
            EndDate = shift.EndDate,
            PricePerUser = shift.PricePerUser,
            ShiftTypeId = shift.ShiftTypeId,
            CreatorUserId = shift.CreatorUserId,
            MaxUsers = shift.MaxUsers,
            TotalPrice = shift.TotalPrice,
            Status = shift.Status
        };
    }

    //public async Task<bool> UpdateShift(int shiftId, Shift shift)
    //{
    //    using var context = _contextFactory.CreateDbContext();
    //    var existingShift = await context.Shifts.FindAsync(shiftId);
    //    if (existingShift == null)
    //    {
    //        return false;
    //    }
    //    existingShift.StartDate = shift.StartDate;
    //    existingShift.EndDate = shift.EndDate;
    //    existingShift.PricePerUser = shift.PricePerUser;
    //    existingShift.CreatorUserId = shift.CreatorUserId;
    //    existingShift.MaxUsers = shift.MaxUsers;
    //    existingShift.TotalPrice = shift.TotalPrice;
    //    existingShift.Status = shift.Status;
    //    await context.SaveChangesAsync();
    //    return true;
    //}
}

public class ShiftResponse
{
    public int ShiftId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerUser { get; set; }
    public int ShiftTypeId { get; set; }
    public int CreatorUserId { get; set; }
    public int? MaxUsers { get; set; }
    public decimal? TotalPrice { get; set; }
    public int Status { get; set; }
}

public class ShiftRequest
{
    [Required(ErrorMessage = "Start date is required.")]
    public DateTime StartDate { get; set; }
    [Required(ErrorMessage = "End date is required.")]
    public DateTime EndDate { get; set; }
    [Required(ErrorMessage = "Shift type id is required.")]
    public int ShiftTypeId { get; set; }
    public int? MaxUsers { get; set; }
    public decimal? TotalPrice { get; set; }
    [Required(ErrorMessage = "Place id is required.")]
    public int Status { get; set; }
    [Required(ErrorMessage = "Place id is required.")]
    public int PlaceId { get; set; }
}
