using KeysRace.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KeysRace.Infrastructure.Data.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid id);
}

public class UserRepository : IUserRepository
{
    private readonly KeysRaceDbContext _context;

    public UserRepository(KeysRaceDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Profile)
            .SingleOrDefaultAsync(u => u.Id == id);
    }
}