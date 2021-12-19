using System.ComponentModel.DataAnnotations.Schema;

namespace server.Model;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Institution { get; set; }

    public string Degree { get; set; }

    //[ForeignKey("PostId")]
    public ICollection<Post> Posts { get; set; }

    public List<UserPost> UserPosts { get; set; }
}
