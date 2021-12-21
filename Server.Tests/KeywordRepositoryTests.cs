using server.Database;
using server.Model;
using server.Repositories;
using Xunit;

// For Test Coverage:
// CLI: dotnet test /p:CollectCoverage=true

namespace server.Tests
{
    public class KeywordRepositoryTests : IDisposable
    {
        private readonly IContext _context;
        private readonly KeywordRepository _repository;


        public KeywordRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlite(connection);
            var context = new Context(builder.Options);
            context.Database.EnsureCreated();

            Keyword keywordJava = new Keyword { Name = "Java" };
            Keyword keywordMath = new Keyword { Name = "Math" };

            context.Keywords.Add(keywordJava);
            context.Keywords.Add(keywordMath);
            context.SaveChanges();

            _context = context;
            _repository = new KeywordRepository(_context);
        }



        [Fact]
        public void ReadById_given_existing_returns_keyword()
        {
            // arrange
            var expected = new KeywordDTO(1, "Java");

            // act
            var result = _repository.ReadById(1);

            // assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);

        }


        public void ReadById_given_nonexisting_returns_null(int id)
        {
            var result = _repository.ReadById(id);

            Assert.Null(result);
        }



        [Fact]
        public void ReadAllKeywords_returns_all_keywords()
        {
            // act
            var actualKeywords = _repository.ReadAllKeywords();
            // arrange
            // assert
            Assert.Collection(
                actualKeywords,
                    k =>
                    {
                        Assert.Equal(1, k.Id);
                        Assert.Equal("Java", k.Name);
                    },
                    k =>
                    {
                        Assert.Equal(2, k.Id);
                        Assert.Equal("Math", k.Name);
                    }
            );

        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
