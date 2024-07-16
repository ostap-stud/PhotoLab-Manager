using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoLab.Areas.Identity.Data;
using PhotoLab.Models;

namespace PhotoLab.Data;

public class PhotoLabContext : IdentityDbContext<PhotoLabUser>
{
    public PhotoLabContext(DbContextOptions<PhotoLabContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Service>()
            .HasMany(s => s.Orders)
            .WithOne(o => o.Service)
            .HasForeignKey(o => o.ServiceId)
            .IsRequired(true);

        base.OnModelCreating(builder);
    }
}
