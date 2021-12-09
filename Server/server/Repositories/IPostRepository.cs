Namespace Interfaces;

public interface IPostRepository 
{
    Task<(Status, PostDto)> CreateAsync(PostCreateDto post);
    Task<Option<PostDto>> ReadAsync(int PostId);
    Task<IReadOnlyCollection<PostDto>> ReadAsync();
}

