using server.Database;
using server.Model;
using server.Repositories;
using Xunit;

// For Test Coverage:
// CLI: dotnet test /p:CollectCoverage=true /p:Exclude="[ProjectBank.Client]*"

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


        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(100)]
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

        [Fact]
        public void KeywordRepository_HasBeen_Disposed_Throws_ObjectDiscopsedException()
        {
            // Arr
            _repository.Dispose();
        
            // Act
        
            // Assert
            Assert.Throws<System.ObjectDisposedException>(() => _repository.ReadById(1));
        }

        [Fact]
        public void CreateKeyword_with_given_KeywordCreateDTO()
        {
            // Arrange
            KeywordCreateDTO keywordCreateDTO = new KeywordCreateDTO("Graphic Design");
            Keyword expectedKeyword = new Keyword{ Id = 3, Name = "Graphic Design" };

            // Actual
            var createdKeywordId = _repository.Create(keywordCreateDTO);
            var createdKeyword = _context.Keywords.Find(createdKeywordId);

            // Assert
            Assert.Equal(expectedKeyword.Id, createdKeyword.Id);
            Assert.Equal(expectedKeyword.Name, createdKeyword.Name);
        }

        [Fact]
        public void CreateKeyword_with_given_KeywordCreateDTO_Already_Created()
        {
            // Arrange
            KeywordCreateDTO keywordCreateDTO = new KeywordCreateDTO("Java");
            var alreadyExistingKeyword = _context.Keywords.Find(1);

            // Actual
            var actualIdWhenAttemptingCreate = _repository.Create(keywordCreateDTO); // Create returns the Id of an already existing Keyword

            // Assert
            Assert.Equal(alreadyExistingKeyword.Id, actualIdWhenAttemptingCreate);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
