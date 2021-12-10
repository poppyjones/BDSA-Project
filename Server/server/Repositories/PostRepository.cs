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

    public PostDTO Create(PostCreateDTO post)
    {
        //var post = new 
        throw new NotImplementedException();
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

    public PostDTO ReadById(int PostId)
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
    
    public PostDTO ReadByAuthorId(int AuthorId)
    {
        var post =  from p in _context.Posts
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

        return post.FirstOrDefault();
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
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
