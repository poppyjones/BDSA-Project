using server.Model;
using main;
namespace server.Database;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = LoadConfiguration();

        var connectionString = configuration.GetConnectionString("PrimeSlice");

        var optionsBuilder = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString)
            .EnableSensitiveDataLogging();

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

    public static void DBSeed(Context context)
    {
        context.Database.EnsureCreated();
        context.Database.ExecuteSqlRaw("DELETE dbo.PostUser");
        context.Database.ExecuteSqlRaw("DELETE dbo.KeywordPost");
        context.Database.ExecuteSqlRaw("DELETE dbo.Keywords");
        context.Database.ExecuteSqlRaw("DELETE dbo.Posts");
        context.Database.ExecuteSqlRaw("DELETE dbo.Users");
        
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Keywords', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");

        Keyword keyword1 = new Keyword { Name = "C#" };
        Keyword keyword2 = new Keyword { Name = "Databases" };

        context.Keywords.Add(keyword1);
        context.Keywords.Add(keyword2);

        context.SaveChanges();

        Post post = new Post {  Title = "The quick brown fox jumps over the lazy dog",
                                AuthorId = 1,
                                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                                Created = DateTime.UtcNow,
                                Ended = DateTime.UtcNow,
                                Status = "Ended",
                                Keywords = new List<Keyword> { keyword1, keyword2 } };

        context.Posts.Add(post);
        context.SaveChanges();

        User user = new User {  Name = "Eric",
                                Email = "Eric@mail.dk",
                                Degree = "SWU",
                                Institution = "ITU",
                                Posts = new List<Post> { post } };

        context.Users.Add(user);

        context.SaveChanges();
    }
}