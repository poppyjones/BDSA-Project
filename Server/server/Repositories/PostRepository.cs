namespace Repository;

public class PostRepository : IPostRepository
{

    private readonly IContext _context;

    public PostRepository(IContext context)
    {
        _context = context;
    }

    Task<(Status, PostDto)> CreateAsync(PostCreateDto post)
    {
        throw NotImplementedException();
    }

    Task<Option<PostDto>> ReadAsync(int PostId)
    {
        throw NotImplementedException();
    }

    Task<IReadOnlyCollection<PostDto>> ReadAsync()
    {
        throw NotImplementedException();
    }
    
}
