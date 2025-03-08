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
}
