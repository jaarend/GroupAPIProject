using System.Dynamic;
using GroupApiProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GroupApiProject.Data;

public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<int>, int>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<CharacterEntity> Characters {get; set;} = null!;
    public DbSet<ClassEntity> Classes {get; set;} = null!;
    public DbSet<GearEntity> Gear {get; set;} = null!;
    public DbSet<RaceEntity> Races {get; set;} = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserEntity>().ToTable("Users");
    }

}