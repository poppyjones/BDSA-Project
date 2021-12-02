namespace Model;

public class Supervisor : User
    {
        public Collection<Post> ownedPosts { get; set; }
    }
