using server.Interfaces;
using server.Database;
using server.Model;
namespace server.Repositories;

public class PostRepository : IPostRepository
{

    private readonly IContext _context;

    public PostRepository(IContext context)
    {
        _context = context;
    }

    public int Create(PostCreateDTO post)
    {
        var newPost = new Post{ 
                                Title = post.Title,
                                AuthorId = post.AuthorId,
                                Created = post.Created,
                                Ended = post.Ended,
                                Status = post.Status,
                                Description = post.Description,
                                Keywords = KeywordDTOsToKeywords(post.Keywords).ToList(),
                                Users = UserDTOsToUsers(post.Users).ToList()
                            };

        _context.Posts.Add(newPost);
        _context.SaveChanges();

        return newPost.PostId;
    }

    /*public (Response Response, int UserId) Create(UserCreateDTO user)
    {
        if (ReadByEmail(user.Email) != null)
        {
            return (Conflict, 0);
        }

        var entity = new User
        {
            Name = user.Name,
            Email = user.Email
        };

        _context.Users.Add(entity);
        _context.SaveChanges();

        return (Response.Created, entity.Id);
    }*/

    public PostDTO ReadByPostId(int PostId)
    {
        var post =  from p in _context.Posts
                    where p.PostId == PostId
                    select new PostDTO(
                        p.PostId,
                        p.Title,
                        p.AuthorId,
                        p.Created,
                        p.Ended,
                        p.Status,
                        p.Description,
                        KeywordsToKeywordDTOs(p.Keywords).ToList(),
                        UsersToUserDTOs(p.Users).ToList()
                    );
        
        return post.FirstOrDefault();
    }
    
    public ICollection<PostDTO> ReadAllByAuthorId(int AuthorId)
    {
        var posts = from p in _context.Posts
                    where p.AuthorId == AuthorId
                    select new PostDTO(
                        p.PostId,
                        p.Title,
                        p.AuthorId,
                        p.Created,
                        p.Ended,
                        p.Status,
                        p.Description,
                        KeywordsToKeywordDTOs(p.Keywords).ToList(),
                        UsersToUserDTOs(p.Users).ToList()
                    );

        return posts.ToList();
    }

    private static IEnumerable<KeywordDTO> KeywordsToKeywordDTOs(ICollection<Keyword> Keywords)
    {
        foreach (Keyword keyword in Keywords)
        {
            yield return new KeywordDTO(keyword.KeywordId, keyword.Name);            
        }
    }

    private static IEnumerable<UserDTO> UsersToUserDTOs(ICollection<User> Users)
    {
        foreach (User user in Users)
        {
            yield return new UserDTO(user.UserId, user.Name, user.Email, user.Institution, user.Degree);
        }
    }

    private static IEnumerable<Keyword> KeywordDTOsToKeywords(ICollection<KeywordDTO> Keywords)
    {
        foreach (KeywordDTO keyword in Keywords)
        {
            yield return new Keyword { KeywordId = keyword.KeywordId, Name = keyword.Name };            
        }
    }

    private static IEnumerable<User> UserDTOsToUsers(ICollection<UserDTO> Users)
    {
        foreach (UserDTO user in Users)
        {
            yield return new User { UserId = user.UserId, Name = user.Name, Email = user.Email, Institution = user.Institution, Degree = user.Degree };
        }
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
