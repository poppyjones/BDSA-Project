using server.Database;
using server.Model;
using server.Repositories;

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
        // var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        // var date2 = new DateTime(2009, 6, 6, 6, 0, 0);
        
        // var posts = ICollection<Post> (
        //                         new Post { "1", "firstPost", "1", date1, "", "Active", "DescriptiveDescription", keywords, users},
        //                         new Post {"1", "firstPost", "1", date2, "", "Active", "DescriptiveDescription", keywords, users}
        //                         );

        // Adding data to test area
        context.Users.Add( new User
        {
            Name = "Eric",
            Email = "Eric@mail.dk",
            Degree = "bsc science",
            Institution = "ITU"//,
            //posts = posts
        });


        context.SaveChanges();

        _context = context;
        _repository = new UserRepository(_context);
    }

    [Fact]
    public void ReadById_given_existing_userid_returns_user()
    {
        // arrange
        

        // act
        var result =  _repository.ReadById(1);
        
        // assert
        Assert.Equal(new UserDTO(1, "Eric", "Eric@mail.dk", "ITU", "bsc science"), result);
    }

    [Fact]
    public void ReadById_given_nonexisting_userid_returns_null()
    {
        var result =  _repository.ReadById(100);

        Assert.Null(result);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
