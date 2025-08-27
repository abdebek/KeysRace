using KeysRace.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KeysRace.Infrastructure.Data.Repositories
{
    public interface IGameRoomRepository
    {
        void Add(GameRoom gameRoom);
        Task<GameRoom?> GetByIdAsync(Guid id);
    }

    public class GameRoomRepository : IGameRoomRepository
    {
        private readonly KeysRaceDbContext _context;

        public GameRoomRepository(KeysRaceDbContext context)
        {
            _context = context;
        }

        public void Add(GameRoom gameRoom)
        {
            _context.GameRooms.Add(gameRoom);
        }

        public async Task<GameRoom?> GetByIdAsync(Guid id)
        {
            return await _context.GameRooms
                .Include(gr => gr.PlayerSessions)
                .SingleOrDefaultAsync(gr => gr.Id == id);
        }
    }
}