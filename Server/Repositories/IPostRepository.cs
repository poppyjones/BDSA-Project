using server.Database;
using server.Model;

namespace server.Interfaces;

public interface IPostRepository 
{
    int Create(PostCreateDTO post);
    PostDTO ReadByPostId(int PostId);
    ICollection<PostDTO> ReadAllByAuthorId(int AuthorId);
}

