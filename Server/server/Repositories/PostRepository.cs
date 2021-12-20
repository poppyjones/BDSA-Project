using server.Interfaces;
using server.Database;
using server.Model;
namespace server.Repositories;

public class PostRepository //: IPostRepository
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

        return newPost.Id;
    }

    public PostDTO ReadByPostId(int PostId)
    {
        var post =  from p in _context.Posts
                    where p.Id == PostId
                    select new PostDTO(
                        p.Id,
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
                        p.Id,
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
            yield return new KeywordDTO(keyword.Id, keyword.Name);            
        }
    }

    private static IEnumerable<UserDTO> UsersToUserDTOs(ICollection<User> Users)
    {
        foreach (User user in Users)
        {
            yield return new UserDTO(user.Id, user.Name, user.Email, user.Institution, user.Degree);
        }
    }

    private static IEnumerable<Keyword> KeywordDTOsToKeywords(ICollection<KeywordDTO> Keywords)
    {
        foreach (KeywordDTO keyword in Keywords)
        {
            yield return new Keyword { Id = keyword.Id, Name = keyword.Name };            
        }
    }

    private static IEnumerable<User> UserDTOsToUsers(ICollection<UserDTO> Users)
    {
        foreach (UserDTO user in Users)
        {
            yield return new User { Id = user.Id, Name = user.Name, Email = user.Email, Institution = user.Institution, Degree = user.Degree };
        }
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
