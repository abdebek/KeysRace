using KeysRace.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KeysRace.Infrastructure.Data.Repositories;

public interface ITypingTextRepository
{
    Task<TypingText?> GetByIdAsync(Guid id);
    Task<TypingText> GetRandomAsync();
}

public class TypingTextRepository : ITypingTextRepository
{
    private readonly KeysRaceDbContext _context;

    public TypingTextRepository(KeysRaceDbContext context)
    {
        _context = context;
    }

    public async Task<TypingText?> GetByIdAsync(Guid id)
    {
        return await _context.TypingTexts.FindAsync(id);
    }

    public async Task<TypingText> GetRandomAsync()
    {
        return await _context.TypingTexts
            .OrderBy(r => Guid.NewGuid())
            .FirstAsync();
    }
}