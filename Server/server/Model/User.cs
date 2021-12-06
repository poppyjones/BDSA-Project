namespace Model;

public class User
{
    // public User()
    // {
    //     collaborating_posts = new HashSet<Post>();
    // }
    // public User(int id, string name, EmailAddressAttribute email, string institution, string degree, ICollection<Post> collaborating_posts)
    // {
    //     this.id = id;
    //     this.name = name;
    //     this.email = email;
    //     this.institution = institution;
    //     this.degree = degree;
    //     this.collaborating_posts = collaborating_posts;
    // }

    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    //[DataType(DataType.EmailAddress)]
    //public EmailAddressAttribute Email { get; set; }

    public string Institution { get; set; }

    public string Degree { get; set; }

    public ICollection<UserPost> UserPost { get; set; }
}
