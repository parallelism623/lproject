using lproject.EFCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace lproject.EFCore;

public static class Excute
{
    public static void EfCoreRegisterServices(this IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("EfCoreSql");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
    }
    public static async Task GeneratedValueRunTest(IServiceProvider services)
    {
        await using var scoped = services.CreateAsyncScope();
        var dbContext = scoped.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
        
        var entity = new EntityGenerateValue () {
            Id = Guid.NewGuid(), 			
            Computed = "INSERT (Ignore)",
            Concurrency = "INSERT (Save)",
            DefaultValue = "INSERT (Save)",
            RowVersion = "Insert_RowVersion",
            ValueGeneratedNever = "INSERT (Save)",
            ValueGeneratedOnAdd = "INSERT (Save)",
            ValueGeneratedOnAddOrUpdate = "INSERT (Ignore)",
            ValueGeneratedOnUpdate = "INSERT (Save)",
            ValueGeneratedOnUpdateSometimes = "INSERT (Save)"
        };
        
        dbContext.EntityGenerateValues.Add(entity);
        await dbContext.SaveChangesAsync();
        Console.WriteLine("Insert");
        Console.WriteLine(entity);
        
        // ChangeTracker have been checking the entity.
        entity.Computed = "UPDATE (Ignore)"; // Value will be ignored by EF Core.
        entity.Concurrency = "UPDATE (Save)"; // Value will be used to generate SQL update command.
        entity.DefaultValue = "UPDATE (Save)";
        entity.RowVersion = "Update_RowVersion";
        entity.ValueGeneratedNever = "UPDATE (Save)";
        entity.ValueGeneratedOnAdd = "UPDATE (Save)";
        entity.ValueGeneratedOnAddOrUpdate = "UPDATE (Ignore)";
        entity.ValueGeneratedOnUpdate = "UPDATE (Ignore)";
        entity.ValueGeneratedOnUpdateSometimes = "UPDATE (Save)";
			
        await dbContext.SaveChangesAsync();
        Console.WriteLine("Update");
        Console.WriteLine(entity);
    }
}