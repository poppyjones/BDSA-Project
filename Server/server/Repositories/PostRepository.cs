using server.Interfaces;
using server.Database;
using server.Model;
namespace server.Repository;

public class PostRepository : IPostRepository
{

    private readonly IContext _context;

    public PostRepository(IContext context)
    {
        _context = context;
    }

    public Task<PostDTO> CreateAsync(PostCreateDTO post)
    {
        throw new NotImplementedException();
    }

    public Task<PostDTO> ReadAsync(int PostId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<PostDTO>> ReadAsync()
    {
        throw new NotImplementedException();
    }
    
}
