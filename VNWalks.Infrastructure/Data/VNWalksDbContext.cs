using Microsoft.EntityFrameworkCore;
using VNWalks.Shared.EntityModels.SqlServer.EntityModels;

namespace VNWalks.Infrastructure.Data;

public class VNWalksDbContext : DbContext
{
    public VNWalksDbContext (DbContextOptions dbContextOptions) : base (dbContextOptions)
    {

    }

    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
    public DbSet<Image> Images { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for Difficulties
        // Easy, Medium, Hard
        var difficulties = new List<Difficulty>
        {
            new Difficulty
            {
                Id = Guid.Parse("5daca6f3-3e6d-40fa-9c54-e51e2fc60b62"),
                Name = "Easy"
            },
            new Difficulty
            {
                Id = Guid.Parse("5223651c-9784-4ad7-a596-bd8cf69a5725"),
                Name = "Medium"
            },
            new Difficulty
            {
                Id = Guid.Parse("747cf14c-a757-4f16-9170-2a2069e78541"),
                Name = "Hard"
            },
        };

        // Seed difficulties to the database
        modelBuilder.Entity<Difficulty>().HasData(difficulties);

        // Seed data for Regions
        var regions = new List<Region>
        {
            new Region
            {
                Id = Guid.Parse("72bae868-bc5c-48f8-a8ab-e29ca99e5dfc"),
                Name = "Nghe An",
                Code = "29",
            },
            new Region
            {
                Id = Guid.Parse("d43c4ba3-c173-4e35-8f59-a98c3adc496c"),
                Name = "Ha Tinh",
                Code = "30",
            },
            new Region
            {
                Id = Guid.Parse("2459c408-89c1-4947-86d9-d2cebff3352b"),
                Name = "Lam Dong",
                Code = "42",
            },
            new Region
            {
                Id = Guid.Parse("42a416b2-36df-4f89-82d7-d77873208da7"),
                Name = "Tien Giang",
                Code = "53",
            },

        };

        modelBuilder.Entity<Region>().HasData(regions);
    }
}
