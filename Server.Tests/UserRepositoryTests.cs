using server.Database;
using server.Model;
using server.Repositories;

// For Test Coverage:
// CLI: dotnet test /p:CollectCoverage=true

namespace server.Tests
{
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

            // Adding data to test area
            context.Users.Add( new User
            {
                Name = "Eric",
                Email = "Eric@mail.dk",
                Degree = "bsc science",
                Institution = "ITU"//,
            });
            context.Users.Add( new User
            {
                Name = "Hans",
                Email = "Hans@mail.dk",
                Degree = "bsc science",
                Institution = "ITU"//,
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
            var result1 = _repository.ReadById(1);
            var result2 = _repository.ReadById(2);
            
            // assert
            Assert.Equal(new UserDTO(1, "Eric", "Eric@mail.dk", "ITU", "bsc science"), result1);
            Assert.Equal(new UserDTO(2, "Hans", "Hans@mail.dk", "ITU", "bsc science"), result2);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(50)]
        [InlineData(100)]
        public void ReadById_given_nonexisting_userid_returns_null(int id)
        {
            // arrange

            // act
            var result =  _repository.ReadById(id);

            // assert
            Assert.Null(result);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}