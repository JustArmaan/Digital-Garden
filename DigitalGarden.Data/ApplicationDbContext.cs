﻿using DigitalGarden.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCView.Models;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
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

        // Configure the Id column for IdentityRole to use VARCHAR(128)
        modelBuilder.Entity<IdentityRole>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
        });

        // Configure the Id column for IdentityUser to use VARCHAR(128)
        modelBuilder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(e => e.Id)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
        });

        // Configure the UserId column for AspNetUserTokens to use VARCHAR(128)
        modelBuilder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.Property(e => e.UserId)
                .HasMaxLength(128)
                .HasColumnType("varchar(128)");
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
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascading deletes here

        // Ensure any other foreign key relationships on UserId are handled correctly
        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
