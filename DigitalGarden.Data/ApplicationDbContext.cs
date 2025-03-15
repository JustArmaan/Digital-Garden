using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCView.Models;

namespace DigitalGarden.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Plant> Plants { get; set; }
    public DbSet<CareLog> CareLogs { get; set; }
    public DbSet<CommunityTip> CommunityTips { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Garden> Gardens { get; set; }
    public DbSet<PlantType> PlantTypes { get; set; }
    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Friend> Friends { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<IdentityRole>(entity =>
    {
        entity.Property(e => e.Id)
            .HasColumnType("nvarchar(450)");
    });

    // Friend relationship (prevent cascading deletes)
    modelBuilder.Entity<Friend>()
        .HasOne(f => f.User)
        .WithMany()
        .HasForeignKey(f => f.UserId)
        .OnDelete(DeleteBehavior.Restrict);  // Restrict cascading deletes here

    modelBuilder.Entity<Friend>()
        .HasOne(f => f.FriendUser)
        .WithMany()
        .HasForeignKey(f => f.FriendId)
        .OnDelete(DeleteBehavior.Restrict);  // Restrict cascading deletes here

    // Comment relationship: prevent cascading deletes on UserId in Comments
    modelBuilder.Entity<Comment>()
        .HasOne(c => c.User) // Assuming 'User' is the foreign key
        .WithMany()  // One user can have many comments
        .HasForeignKey(c => c.UserId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes here

    // Ensure any other foreign key relationships on UserId are handled correctly
    modelBuilder.Entity<Post>()
        .HasOne(p => p.User)
        .WithMany()
        .HasForeignKey(p => p.UserId)
        .OnDelete(DeleteBehavior.NoAction); // Prevent cascading delete on Post as well

    // If you have any other relationships involving User or other cascade paths, ensure to specify NoAction or Restrict
}
}
