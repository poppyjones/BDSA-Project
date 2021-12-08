namespace Model;

public class Supervisor : User
{
    public Collection<Post> OwnedPosts { get; set; }

    // public Supervisor (int id, string name, EmailAddressAttribute email, string institution, string degree, ICollection<Post> collaborating_posts, Collection<Post> ownedPosts) : base (id, name, email, institution, degree, collaborating_posts)
    // {
    //     this.ownedPosts = ownedPosts;
    // }

    // public Supervisor(Collection<Post> ownedPosts): base(id, name, email, institution, degree, collaborating_posts)
    // {
    //     this.ownedPosts = ownedPosts;
    // }


}
