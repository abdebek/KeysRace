using Microsoft.AspNetCore.Identity;

namespace KeysRace.Domain;

public abstract class Entity
{
    public Guid Id { get; protected set; }
}

public abstract class AggregateRoot : Entity
{
}

public class User : IdentityUser<Guid>
{
    public DateTime CreatedAt { get; private set; }
    public UserProfile Profile { get; private set; }

    public User()
    {
        CreatedAt = DateTime.UtcNow;
    }
}

public class UserProfile : Entity
{
    public Guid UserId { get; private set; }
    public string DisplayName { get; private set; }
    public User User { get; private set; }
}

public enum GameState { Waiting, Active, Finished }

public class GameRoom : AggregateRoot
{
    public string RoomCode { get; private set; }
    public GameState State { get; private set; }
    public Guid TypingTextId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private readonly List<PlayerSession> _playerSessions = new();
    public IReadOnlyCollection<PlayerSession> PlayerSessions => _playerSessions.AsReadOnly();
}

public class PlayerSession : Entity
{
    public Guid GameRoomId { get; private set; }
    public Guid PlayerId { get; private set; }
    public string DisplayName { get; private set; }

    public int ProgressPercentage { get; private set; }
    public double WordsPerMinute { get; private set; }
}

public class TypingText : AggregateRoot
{
    public string Content { get; private set; }
    public string Author { get; private set; }
    public string Source { get; private set; }
    public int Length { get; private set; }
    public string Language { get; private set; }
    public int Difficulty { get; private set; }
}