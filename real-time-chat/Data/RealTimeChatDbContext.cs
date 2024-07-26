using Microsoft.EntityFrameworkCore;
using real_time_chat.Models;

namespace real_time_chat.Data;

public class RealTimeChatDbContext : DbContext
{
    public RealTimeChatDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Messages)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
}