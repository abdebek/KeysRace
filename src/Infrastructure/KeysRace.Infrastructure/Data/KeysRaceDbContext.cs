using KeysRace.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace KeysRace.Infrastructure.Data
{
    public class KeysRaceDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public KeysRaceDbContext(DbContextOptions<KeysRaceDbContext> options) : base(options) { }

        public DbSet<GameRoom> GameRooms { get; set; }
        public DbSet<TypingText> TypingTexts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserId);

            modelBuilder.Entity<GameRoom>().OwnsMany(gr => gr.PlayerSessions, a =>
            {
                a.WithOwner().HasForeignKey("GameRoomId");
                a.Property<Guid>("Id");
                a.HasKey("Id");
            });
        }
    }
}