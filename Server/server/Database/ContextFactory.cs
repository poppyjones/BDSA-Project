using server.Model;
using main;
namespace server.Database;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = LoadConfiguration();

        var connectionString = configuration.GetConnectionString("PrimeSlice");

        var optionsBuilder = new DbContextOptionsBuilder<Context>();
        optionsBuilder.UseSqlServer(connectionString);

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
        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        context.Database.ExecuteSqlRaw("DELETE dbo.Posts");
        context.Database.ExecuteSqlRaw("DELETE dbo.Keywords");
        context.Database.ExecuteSqlRaw("DELETE dbo.Users");
        context.Database.ExecuteSqlRaw("DELETE dbo.UserPost");
        context.Database.ExecuteSqlRaw("DELETE dbo.KeywordPost");
        
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Keywords', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Users', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.KeywordPost', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.UserPost', RESEED, 0)");

        Post post = new Post {  Title = "blabla",
                                Description = "blablabla",
                                Created = DateTime.UtcNow,
                                Status = "New" };

        User user = new User {  Name = "Eric",
                                Email = "Eric@hotmail.com",
                                Institution = "ITU",
                                Degree = "Pron" };

        post.AuthorId = user.UserId;

        context.Users.Add(user);
        context.Posts.Add(post);

        context.SaveChanges();
    }
}