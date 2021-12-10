using server.Database;
using server.Model;
using server.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace server.Tests;

public class PostRepositoryTests : IDisposable
{
    private readonly IContext _context;
    private readonly PostRepository _repository;

    // Test data
    DateTime date1 = new DateTime(2008, 3, 1, 7, 0, 0);
    DateTime date2 = new DateTime(2009, 6, 6, 6, 0, 0);
    
    public PostRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseSqlite(connection);
        var context = new Context(builder.Options);
        context.Database.EnsureCreated();

        var user1 = new User
        {
            Name = "Eric",
            Email = "Erica@mail.dk",
            Degree = "bsc science",
            Institution = "ITU"
        };
        var user2 = new User
        {
            Name = "Erica",
            Email = "Erica@mail.dk",
            Degree = "phd",
            Institution = "ITU"
        };

        context.Users.Add(user1);
        context.Users.Add(user2);

        // Adding data to test area
        context.Posts.Add(new Post
        {
            Title = "MyPost",
            AuthorId = 1,
            Created = date1,
            Ended = date2,
            Status = "Active",
            Description = "this is a description",
            Keywords = null,
            Users = null
        });


        context.SaveChanges();

        _context = context;
        _repository = new PostRepository(_context);
    }

    // Test PostDTO ReadById(int PostId);

    [Fact]
    public void ReadById_given_existing_postid_returns_post()
    {
        // arrange
        var expected = new PostDTO(1, "MyPost", 1, date1, date2, "Active", "this is a description", new Collection<KeywordDTO> {}, new Collection<UserDTO> {});

        // act
        var result = _repository.ReadById(1);

        // assert
        Assert.Equal(expected.Id, result.Id);
        Assert.Equal(expected.Title, result.Title);
        Assert.Equal(expected.AuthorId, result.AuthorId);
        Assert.Equal(expected.Created, result.Created);
        Assert.Equal(expected.Ended, result.Ended);
        Assert.Equal(expected.Status, result.Status);
        Assert.Equal(expected.Description, result.Description);
        CollectionAssert.AreEquivalent(expected.Keywords, result.Keywords);
        CollectionAssert.AreEquivalent(expected.Users, result.Users);
    }

    [Fact]
    public void ReadById_given_nonexisting_postid_returns_null()
    {
        var result = _repository.ReadById(100);

        Assert.Null(result);
    }

    // Test PostDTO ReadByAuthorId(int PostId);

    // Test PostDTO Create(PostCreateDTO post);

    public void Dispose()
    {
        _context.Dispose();
    }
}