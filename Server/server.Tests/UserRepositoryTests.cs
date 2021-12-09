namespace server.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly IContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseSqlite(connection);
        var context = new Context(builder.Options);
        context.Database.EnsureCreated();

        // Test data
        var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        var date2 = new DateTime(1969, 6, 6, 6, 0, 0);
        
        var posts = ICollection<Post> (
                                new Post { "1", "firstPost", "1", date1, "", "Active", "DescriptiveDescription", keywords, users},
                                new Post {"1", "firstPost", "1", date1, "", "Active", "DescriptiveDescription", keywords, users}
                                ); 

        // Adding data to test area
        context.Users.Add( new User
        {
            name = "Eric",
            degree = "swu",
            institution = "ITU",
            posts = posts
        });


        context.SaveChanges();

        _context = context;
        _repository = new UserRepository(_context);
    }

    [Fact]
    public async Task ReadAsync_given_existing_id_returns_user()
    {
        var option = await _repository.ReadAsync(1);

        Assert.Equal(new UserDto(1, "Eric", "swu", ITU), option.Value);
    }

    [Fact]
    public async Task ReadAsync_given_nonexisting_id_returns_null()
    {
        var option = await _repository.ReadAsync(1);

        Assert.Null(option.Value);
    }

}
