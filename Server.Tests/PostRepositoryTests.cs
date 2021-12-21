using server.Database;
using server.Model;
using server.Repositories;
using Xunit;

// For Test Coverage:
// CLI: dotnet test /p:CollectCoverage=true

namespace server.Tests
{
    public class PostRepositoryTests : IDisposable
    {
        private readonly IContext _context;
        private readonly PostRepository _repository;
        private readonly KeywordRepository _keywordRepository;

        // Test data
        DateTime date1 = new DateTime(2008, 3, 1, 7, 0, 0);
        DateTime date2 = new DateTime(2009, 6, 6, 6, 0, 0);

        Collection<Keyword> keywords;
        Collection<KeywordDTO> keywordDTOs;
        Collection<User> users;
        Collection<UserDTO> userDTOs;
        Collection<Post> posts;
        Collection<PostDTO> postDTOs;

        public PostRepositoryTests()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<Context>();
            builder.UseSqlite(connection);
            var context = new Context(builder.Options);
            context.Database.EnsureCreated();

            keywords = new Collection<Keyword> {new Keyword { Name = "C#" },
                                                new Keyword { Name = "Database" },
                                                new Keyword { Name = "Backend" } };

            keywordDTOs = new Collection<KeywordDTO> {  new KeywordDTO(1, "C#"),
                                                        new KeywordDTO(2, "Database"),
                                                        new KeywordDTO(3, "Backend") };

            context.Keywords.Add(keywords[0]);
            context.Keywords.Add(keywords[1]);
            context.Keywords.Add(keywords[2]);

            users = new Collection<User> {  new User { Name = "Eric", Email = "Eric@mail.dk", Institution = "ITU", Degree = "bsc science" },
                                            new User { Name = "Erica", Email = "Erica@mail.dk", Institution = "ITU", Degree = "phd"},
                                            new User { Name = "Hans", Email = "Hans@mail.dk", Institution = "ITU", Degree = "master science"} }; 

            userDTOs = new Collection<UserDTO> {new UserDTO(1, "Eric", "Eric@mail.dk", "ITU", "bsc science" ),
                                                new UserDTO(2, "Erica", "Erica@mail.dk", "ITU", "phd"),
                                                new UserDTO(3, "Hans", "Hans@mail.dk", "ITU", "master science") };

            context.Users.Add(users[0]);
            context.Users.Add(users[1]);
            context.Users.Add(users[2]);

            posts = new Collection<Post> {  new Post
                                            {
                                                Title = "MyPost",
                                                AuthorId = 1,
                                                Created = date1,
                                                Ended = date2,
                                                Status = "Active",
                                                Description = "C# and Database",
                                                Keywords = new List<Keyword> {keywords[0] , keywords[1] },
                                                Users = new List<User> { users[2] }
                                            },
                                            new Post
                                            {
                                                Title = "MyOtherPost",
                                                AuthorId = 1,
                                                Created = date1,
                                                Ended = date2,
                                                Status = "Pending",
                                                Description = "Backend and C#",
                                                Keywords = new List<Keyword> { keywords[0], keywords[2] },
                                                Users = new List<User> { users[1] }
                                            },
                                            new Post
                                            {
                                                Title = "TheThirdOtherPost",
                                                AuthorId = 2,
                                                Created = date1,
                                                Ended = date2,
                                                Status = "Pending",
                                                Description = "Backend and Database",
                                                Keywords = new List<Keyword> { keywords[2], keywords[1] },
                                                Users = new List<User> { users[0] }
                                            } };
            
            postDTOs = new Collection<PostDTO> {new PostDTO(1, "MyPost", 1, date1, date2, "Active", "C# and Database", 
                                                            new Collection<KeywordDTO> { keywordDTOs[0], keywordDTOs[1] },
                                                            new Collection<UserDTO> { userDTOs[2] } ),
                                                new PostDTO(2, "MyOtherPost", 1, date1, date2, "Pending", "Backend and C#",
                                                            new Collection<KeywordDTO> { keywordDTOs[0], keywordDTOs[2] },
                                                            new Collection<UserDTO> { userDTOs[1] } ),
                                                new PostDTO(3, "TheThirdOtherPost", 2, date1, date2, "Pending", "Backend and Database",
                                                            new Collection<KeywordDTO> { keywordDTOs[2], keywordDTOs[1] },
                                                            new Collection<UserDTO> { userDTOs[0] } ) };

            context.Posts.Add(posts[0]);
            context.Posts.Add(posts[1]);
            context.Posts.Add(posts[2]);

            context.SaveChanges();

            _context = context;
            _repository = new PostRepository(_context);
            _keywordRepository = new KeywordRepository(_context);
        }


        [Fact]
        public void ReadByPostId_given_existing_postid_returns_post()
        {
            // arrange
            var expected = new PostDTO( 1, "MyPost", 1, date1, date2, "Active", "C# and Database",
                                        new Collection<KeywordDTO> { keywordDTOs[0], keywordDTOs[1] },
                                        new Collection<UserDTO> { userDTOs[2] } );

            // act
            var result = _repository.ReadByPostId(1);

            // assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Title, result.Title);
            Assert.Equal(expected.AuthorId, result.AuthorId);
            Assert.Equal(expected.Created, result.Created);
            Assert.Equal(expected.Ended, result.Ended);
            Assert.Equal(expected.Status, result.Status);
            Assert.Equal(expected.Description, result.Description);
            Assert.Collection(result.Keywords,
                t => {
                    Assert.Equal(keywordDTOs[0].Id, t.Id);
                    Assert.Equal(keywordDTOs[0].Name, t.Name);
                },
                t => {
                    Assert.Equal(keywordDTOs[1].Id, t.Id);
                    Assert.Equal(keywordDTOs[1].Name, t.Name);
                }
            );
            Assert.Collection(result.Users,
                t => {
                    Assert.Equal(userDTOs[2].Id, t.Id);
                    Assert.Equal(userDTOs[2].Name, t.Name);
                    Assert.Equal(userDTOs[2].Email, t.Email);
                    Assert.Equal(userDTOs[2].Degree, t.Degree);
                    Assert.Equal(userDTOs[2].Institution, t.Institution);
                }
            );
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(50)]
        [InlineData(100)]
        public void ReadByPostId_given_nonexisting_postid_returns_null(int id)
        {
            var result = _repository.ReadByPostId(id);

            Assert.Null(result);
        }

        [Fact]
        public void ReadAllByAuthorId_given_existing_authorid_with_posts_returns_posts()
        {
            // arrange

            // act
            var actualPosts = _repository.ReadAllByAuthorId(1);

            // assert
            Assert.Collection(actualPosts,
                t => {
                    Assert.Equal(postDTOs[0].Id, t.Id);
                    Assert.Equal(postDTOs[0].Title, t.Title);
                    Assert.Equal(postDTOs[0].AuthorId, t.AuthorId);
                    Assert.Equal(postDTOs[0].Created, t.Created);
                    Assert.Equal(postDTOs[0].Ended, t.Ended);
                    Assert.Equal(postDTOs[0].Status, t.Status);
                    Assert.Equal(postDTOs[0].Description, t.Description);
                },
                t => {
                    Assert.Equal(postDTOs[1].Id, t.Id);
                    Assert.Equal(postDTOs[1].Title, t.Title);
                    Assert.Equal(postDTOs[1].AuthorId, t.AuthorId);
                    Assert.Equal(postDTOs[1].Created, t.Created);
                    Assert.Equal(postDTOs[1].Ended, t.Ended);
                    Assert.Equal(postDTOs[1].Status, t.Status);
                    Assert.Equal(postDTOs[1].Description, t.Description);
                }
            );
        }

        [Fact]
        public void ReadAllByAuthorId_given_existing_authorid_without_posts_returns_empty_list()
        {
            // arrange
            var expected = new List<PostDTO>();            

            // act
            var actual = _repository.ReadAllByAuthorId(4);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatePost_with_given_PostCreateDTO()
        {
            // arrange
            Keyword keywordJava = new Keyword { Name = "Java" };
            Keyword keywordMath = new Keyword { Name = "Math" };

            _context.Keywords.Add(keywordJava);
            _context.Keywords.Add(keywordMath);
            _context.SaveChanges();

            KeywordDTO keywordDTOJava = _keywordRepository.ReadById(4);
            KeywordDTO keywordDTOMath = _keywordRepository.ReadById(5);

            var expectedPost = new Post
            {
                Id = 4,
                Title = "TheFourthPost",
                AuthorId = 3,
                Created = date2,
                Ended = date1,
                Status = "Ended",
                Description = "Java and Math",
                Keywords = new Collection<Keyword> { keywordJava, keywordMath }
            };
            var postToBeCreated = new PostCreateDTO
            (
                "TheFourthPost",
                3,
                date2,
                date1,
                "Ended",
                "Java and Math",
                new Collection<KeywordDTO> { keywordDTOJava, keywordDTOMath }
            );

            // act
            var postId = _repository.Create(postToBeCreated);
            var actualPost = _context.Posts.Find(postId);

            // assert
            Assert.NotNull(actualPost);
            Assert.Equal(expectedPost.Id, actualPost.Id);
            Assert.Equal(expectedPost.Title, actualPost.Title);
            Assert.Equal(expectedPost.AuthorId, actualPost.AuthorId);
            Assert.Equal(expectedPost.Created, actualPost.Created);
            Assert.Equal(expectedPost.Ended, actualPost.Ended);
            Assert.Equal(expectedPost.Status, actualPost.Status);
            Assert.Equal(expectedPost.Description, actualPost.Description);
            Assert.Collection(actualPost.Keywords,
                t => {
                    Assert.Equal(keywordJava.Id, t.Id);
                    Assert.Equal(keywordJava.Name, t.Name);
                },
                t => {
                    Assert.Equal(keywordMath.Id, t.Id);
                    Assert.Equal(keywordMath.Name, t.Name);
                }
            );
        }

        [Fact]
        public void PostRepository_HasBeen_Disposed_Throws_ObjectDiscopsedException()
        {
            // Arr
            _repository.Dispose();
        
            // Act
        
            // Assert
            Assert.Throws<System.ObjectDisposedException>(() => _repository.ReadByPostId(1));
        }
        

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}