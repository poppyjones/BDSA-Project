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
                                Status = post.Status,
                                Description = post.Description,
                                Keywords = KeywordDTOsToKeywords(post.Keywords, _context).ToList(),

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
                        KeywordsToKeywordDTOs(p.Keywords, _context).ToList(),
                        UsersToUserDTOs(p.Users, _context).ToList()
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
                        KeywordsToKeywordDTOs(p.Keywords, _context).ToList(),
                        UsersToUserDTOs(p.Users, _context).ToList()
                    );

        return posts.ToList();
    }

    private static IEnumerable<KeywordDTO> KeywordsToKeywordDTOs(ICollection<Keyword> Keywords, IContext context)
    {
        foreach (Keyword keyword in Keywords)
        {
            var result = from k in context.Keywords
                            where k.Id == keyword.Id
                            select new KeywordDTO(
                                k.Id,
                                k.Name
                            );
            yield return result.FirstOrDefault();
        }
    }

    private static IEnumerable<UserDTO> UsersToUserDTOs(ICollection<User> Users, IContext context)
    {
        foreach (User user in Users)
        {
            var result = from u in context.Users
                            where u.Id == user.Id
                            select new UserDTO(
                                user.Id,
                                user.Name,
                                user.Email,
                                user.Institution,
                                user.Degree
                            );
            yield return result.FirstOrDefault();
        }
    }

    private static IEnumerable<Keyword> KeywordDTOsToKeywords(ICollection<KeywordDTO> Keywords, IContext context)
    {
        foreach (KeywordDTO keyword in Keywords)
        {
            var result = from k in context.Keywords
                            where k.Id == keyword.Id
                            select k;
            yield return result.FirstOrDefault();
        }
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}
