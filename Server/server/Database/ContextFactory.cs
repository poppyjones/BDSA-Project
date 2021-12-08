using Model;
namespace Database;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = LoadConfiguration();

        var connectionString = configuration.GetConnectionString("PrimeSlice");

        var optionsBuilder = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString);

        return new Context(optionsBuilder.Options);
    }

    public static IConfiguration LoadConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>();

        return builder.Build();
    }

    public static void Seed(Context context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.PostUser");
        context.Database.ExecuteSqlRaw("DELETE dbo.KeywordPost");
        context.Database.ExecuteSqlRaw("DELETE dbo.Keywords");
        context.Database.ExecuteSqlRaw("DELETE dbo.Posts");
        context.Database.ExecuteSqlRaw("DELETE dbo.Users");
        
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.KeywordPost', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.PostUser', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.PostKeywords', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");

        context.SaveChanges();
    }
}