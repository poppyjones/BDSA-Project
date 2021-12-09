using server.Database;
using server.Model;

namespace server.Interfaces;

public interface IPostRepository 
{
    Task<PostDTO> CreateAsync(PostCreateDTO post);
    Task<PostDTO> ReadAsync(int PostId);
    Task<IReadOnlyCollection<PostDTO>> ReadAsync();
}

