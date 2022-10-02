using CarInfo.Areas.Identity.Data;
using CarInfo.Entities;
using CarInfo.Entities.EntityConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarInfo.Data;


public class CarInfoContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<CarType> CarTypes { get; set; }
    public DbSet<CarModel> Sedans { get; set; }
    public DbSet<SuperCar> SuperCars { get; set; }



    public CarInfoContext(DbContextOptions<CarInfoContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CatalogConfiguration());
        builder.ApplyConfiguration(new CarTypeConfiguration());
        builder.ApplyConfiguration(new CarModelConfiguration());
        builder.ApplyConfiguration(new SuperCarConfiguration());


    }
}

