using lproject.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace lproject.EFCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
    public DbSet<EntityGenerateValue>  EntityGenerateValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntityGenerateValue>().HasKey(x => x.Id);
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.Identity).UseIdentityColumn();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.Computed).HasComputedColumnSql("'Ignore'");
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.Concurrency).IsConcurrencyToken();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.DefaultValue).HasDefaultValue("DefaultValue");
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.RowVersion).IsRowVersion();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.ValueGeneratedNever).ValueGeneratedNever();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.ValueGeneratedOnAdd).ValueGeneratedOnAdd();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.ValueGeneratedOnAddOrUpdate).ValueGeneratedOnAddOrUpdate();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.ValueGeneratedOnUpdate).ValueGeneratedOnUpdate();
        modelBuilder.Entity<EntityGenerateValue>().Property(x => x.ValueGeneratedOnUpdateSometimes).ValueGeneratedOnUpdateSometimes();
        base.OnModelCreating(modelBuilder); 
    }
}