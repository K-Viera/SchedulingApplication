using Microsoft.EntityFrameworkCore;

namespace ShiftScheduling.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<UserDB> Users { get; set; }
    public DbSet<ShiftTypeDB> ShiftTypes { get; set; }
    public DbSet<PlaceDB> Places { get; set; }
    public DbSet<ShiftDB> Shifts { get; set; }
    public DbSet<ShiftPlaceDB> ShiftPlaces { get; set; }
    public DbSet<AppliedUserDB> AppliedUsers { get; set; }
    public DbSet<ApprovedUserDB> ApprovedUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserDB>()
            .HasMany(u => u.CreatedShifts)
            .WithOne(s => s.CreatorUser)
            .HasForeignKey(s => s.CreatorUserId);

        modelBuilder.Entity<ShiftTypeDB>()
            .HasMany(st => st.Shifts)
            .WithOne(s => s.ShiftType)
            .HasForeignKey(s => s.ShiftTypeId);

        modelBuilder.Entity<ShiftPlaceDB>()
            .HasKey(sp => new { sp.ShiftId, sp.PlaceId });

        modelBuilder.Entity<ShiftPlaceDB>()
            .HasOne(sp => sp.Shift)
            .WithMany(s => s.ShiftPlaces)
            .HasForeignKey(sp => sp.ShiftId);

        modelBuilder.Entity<ShiftPlaceDB>()
            .HasOne(sp => sp.Place)
            .WithMany(p => p.ShiftPlaces)
            .HasForeignKey(sp => sp.PlaceId);

        modelBuilder.Entity<AppliedUserDB>()
            .HasKey(au => new { au.ShiftId, au.UserId });

        modelBuilder.Entity<AppliedUserDB>()
            .HasOne(au => au.Shift)
            .WithMany(s => s.AppliedUsers)
            .HasForeignKey(au => au.ShiftId);

        modelBuilder.Entity<AppliedUserDB>()
            .HasOne(au => au.User)
            .WithMany(u => u.AppliedShifts)
            .HasForeignKey(au => au.UserId);

        modelBuilder.Entity<ApprovedUserDB>()
            .HasKey(apu => new { apu.ShiftId, apu.UserId });

        modelBuilder.Entity<ApprovedUserDB>()
            .HasOne(apu => apu.Shift)
            .WithMany(s => s.ApprovedUsers)
            .HasForeignKey(apu => apu.ShiftId);

        modelBuilder.Entity<ApprovedUserDB>()
            .HasOne(apu => apu.User)
            .WithMany(u => u.ApprovedShifts)
            .HasForeignKey(apu => apu.UserId);
    }
}
