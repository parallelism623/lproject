using Microsoft.EntityFrameworkCore;

namespace lproject.Data.Replication.Entities;

public class FollowerDbContext(DbContextOptions<FollowerDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");

        base.OnModelCreating(modelBuilder);
    }

}