using Model;
using Main;
namespace Database;

public class ContextFactory : IDesignTimeDbContextFactory<Context>
{
    public Context CreateDbContext(string[] args)
    {
        var configuration = LoadConfiguration();

        var connectionString = configuration.GetConnectionString("PrimeSlice");

        var optionsBuilder = new DbContextOptionsBuilder<Context>()
            .UseSqlServer(connectionString);

        var context = new Context(optionsBuilder.Options);

        //Seed(context);

        return context;
    }

    static IConfiguration LoadConfiguration()
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
        //context.Database.ExecuteSqlRaw("DELETE dbo.PostUser");
        //context.Database.ExecuteSqlRaw("DELETE dbo.KeywordPost");
        context.Database.ExecuteSqlRaw("DELETE dbo.Keywords");
        context.Database.ExecuteSqlRaw("DELETE dbo.Posts");
        context.Database.ExecuteSqlRaw("DELETE dbo.Supervisors");
        ///context.Database.ExecuteSqlRaw("DELETE dbo.Users");
        
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.KeywordPost', RESEED, 0)");
        //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.PostUser', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Keywords', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Posts', RESEED, 0)");
        context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('dbo.Supervisors', RESEED, 0)");


        Supervisor supervisor1 = new Supervisor {  SupervisorId = 1,
                    Name = "Eric",
                    Email = "Eric@mail.com",
                    Institution = "IT University of Copenhagen",
                    Degree = "Bachelor in Pron"};

        Post post1 = new Post {  Title = "",
                            Author = supervisor1,
                            Status = "New",
                            Description = "Bla bla" };

        supervisor1.OwnedPosts.Add(post1);

                
        context.Posts.Add(post1);
        context.Supervisors.Add(supervisor1);

        context.SaveChanges();
    }
}