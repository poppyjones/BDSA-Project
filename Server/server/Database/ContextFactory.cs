using Model;
namespace Database;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddUserSecrets<Program>()
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("PrimeSlice");

        var optionsBuilder = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString);

        return new Context(optionsBuilder.Options);
    }

    public static void Seed(Context context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.Keywords");
        context.Database.ExecuteSqlRaw("DELETE dbo.Posts");
        context.Database.ExecuteSqlRaw("DELETE dbo.Users");
        context.Database.ExecuteSqlRaw("DELETE dbo.Supervisors");
        
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.PostsKeywords', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Supervisors', RESEED, 0)");


        context.SaveChanges();
    }
}