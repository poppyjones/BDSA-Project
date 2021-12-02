using System.Collections.ObjectModel;
using post;
using user;

namespace Supervisor
{
    public class Supervisor : User
    {
        public Collection<Post> ownedPosts { get; set; }
    }
}