using DigitalGarden.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MVCView.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
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
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(
                _configuration.GetConnectionString("DefaultConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Number of retries before failing
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
                        errorNumbersToAdd: null // Use default Azure SQL transient errors
                    );
                });
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
        });

        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
        });

        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.UserId)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
        });

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

        modelBuilder.Entity<Comment>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes here

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
