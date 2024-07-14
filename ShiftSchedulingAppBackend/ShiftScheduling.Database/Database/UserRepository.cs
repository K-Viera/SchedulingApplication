using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftScheduling.Database;

public interface IUserRepository
{
    Task<bool> CheckUserAndPassword(string email, string password);
    Task<string> GetUserRole(string email);
}

public class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<bool> CheckUserAndPassword(string email, string password)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            return false;
        }
        if (user.Password == password)
        {
            return true;
        }
        return false;
    }
    public async Task<string> GetUserRole(string email)
    {
        using var context = await _contextFactory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            return null;
        }
        UserType role = (UserType)user.UserType;
        string roleString = role.ToString();
        return roleString;
    }
}

