using server.Database;
using server.Model;

namespace server.Interfaces;

public interface IPostRepository 
{
    PostDTO Create(PostCreateDTO post);
    PostDTO ReadById(int PostId);
    PostDTO ReadByAuthorId(int PostId);
}

