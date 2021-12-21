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
            var user3 = new User
            {
                Name = "Hans",
                Email = "Hans@mail.dk",
                Degree = "master science",
                Institution = "ITU"
            };

            context.Users.Add(user1);
            context.Users.Add(user2);
            context.Users.Add(user3);

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
            context.Posts.Add(new Post
            {
                Title = "MyOtherPost",
                AuthorId = 1,
                Created = date1,
                Ended = date2,
                Status = "Pending",
                Description = "this is also a description",
                Keywords = null,
                Users = null
            });
            context.Posts.Add(new Post
            {
                Title = "",
                AuthorId = 2,
                Created = date1,
                Ended = date2,
                Status = "Pending",
                Description = "",
                Keywords = null,
                Users = null
            }); 


            context.SaveChanges();

            _context = context;
            _repository = new PostRepository(_context);
        }


        [Fact]
        public void ReadByPostId_given_existing_postid_returns_post()
        {
            // arrange
            var expected = new PostDTO(1, "MyPost", 1, date1, date2, "Active", "this is a description", new Collection<KeywordDTO> { }, new Collection<UserDTO> { });

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
            var expectedPost1 = new Post
            {
                Id = 1,
                Title = "MyPost",
                AuthorId = 1,
                Created = date1,
                Ended = date2,
                Status = "Active",
                Description = "this is a description",
                Keywords = new Collection<Keyword> { },
                Users = new Collection<User> { }
            };

            var expectedPost2 = new Post
            {
                Id = 2,
                Title = "MyOtherPost",
                AuthorId = 1,
                Created = date1,
                Ended = date2,
                Status = "Pending",
                Description = "this is also a description",
                Keywords = new Collection<Keyword> { },
                Users = new Collection<User> { }
            };

            // act
            var actualPosts = _repository.ReadAllByAuthorId(1);

            // assert
            Assert.Collection(actualPosts,
                t => {
                    Assert.Equal(expectedPost1.Id, t.Id);
                    Assert.Equal(expectedPost1.Title, t.Title);
                    Assert.Equal(expectedPost1.AuthorId, t.AuthorId);
                    Assert.Equal(expectedPost1.Created, t.Created);
                    Assert.Equal(expectedPost1.Ended, t.Ended);
                    Assert.Equal(expectedPost1.Status, t.Status);
                    Assert.Equal(expectedPost1.Description, t.Description);
                },
                t => {
                    Assert.Equal(expectedPost2.Id, t.Id);
                    Assert.Equal(expectedPost2.Title, t.Title);
                    Assert.Equal(expectedPost2.AuthorId, t.AuthorId);
                    Assert.Equal(expectedPost2.Created, t.Created);
                    Assert.Equal(expectedPost2.Ended, t.Ended);
                    Assert.Equal(expectedPost2.Status, t.Status);
                    Assert.Equal(expectedPost2.Description, t.Description);
                }
            );
        }

        [Fact]
        public void ReadAllByAuthorId_given_existing_authorid_without_posts_returns_empty_list()
        {
            // arrange

            // act
            var result = _repository.ReadAllByAuthorId(3);
            var actual = Array.Empty<PostDTO>();

            // assert
            Assert.Equal(result, actual);
        }

        [Fact]
        public void CreatePost_with_given_PostCreateDTO()
        {
            // arrange
            var expectedPost = new Post
            {
                Id = 4,
                Title = "MyThirdPost",
                AuthorId = 2,
                Created = date2,
                Ended = date1,
                Status = "Ended",
                Description = "description a is this",
                Keywords = new Collection<Keyword> { },
                Users = new Collection<User> { }
            };
            var postToBeCreated = new PostCreateDTO(
                "MyThirdPost",
                2,
                date2,
                date1,
                "Ended",
                "description a is this",
                new Collection<KeywordDTO> { },
                new Collection<UserDTO> { }
            );

            // act
            var postId = _repository.Create(postToBeCreated);
            var actualPost = _context.Posts.Find(postId);

            // assert
            Assert.Equal(expectedPost.Id, actualPost.Id);
            Assert.Equal(expectedPost.Title, actualPost.Title);
            Assert.Equal(expectedPost.AuthorId, actualPost.AuthorId);
            Assert.Equal(expectedPost.Created, actualPost.Created);
            Assert.Equal(expectedPost.Ended, actualPost.Ended);
            Assert.Equal(expectedPost.Status, actualPost.Status);
            Assert.Equal(expectedPost.Description, actualPost.Description);
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}