using server.Database;
using server.Model;
using server.Repositories;

namespace server.Tests;

public class PostRepositoryTests : IDisposable
{
    private readonly IContext _context;
    private readonly PostRepository _repository;

    public PostRepositoryTests()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var builder = new DbContextOptionsBuilder<Context>();
        builder.UseSqlite(connection);
        var context = new Context(builder.Options);
        context.Database.EnsureCreated();

        // Test data
        // var date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        // var date2 = new DateTime(1969, 6, 6, 6, 0, 0);
        
        // var users = new ICollection<User>();
        // var keywords = new ICollection<Keyword>();

        // Adding data to test area
        // context.Posts.Add(new Post
        // {
        //     Id = "1",
        //     Title = "MyPost",
        //     AuthorId = "1",
        //     Created = date1,
        //     Ended = date2,
        //     Status = "Active",
        //     Description = "DescriptiveDescription",
        //     Keywords = keywords,
        //     Users = users
        // });


        context.SaveChanges();

        _context = context;
        _repository = new PostRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}