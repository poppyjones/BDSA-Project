using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class User
{
    [Key]
    public int UserId { get; set; }

    public string Name { get; set; }

    //[Required]
    //[EmailAddress]
    //public EmailAddressAttribute Email { get; set; }

    public string Institution { get; set; }

    public string Degree { get; set; }

    //[ForeignKey("PostId")]
    public virtual ICollection<Post> CollaboratingPosts { get; set; }

    public virtual ICollection<Post> OwnedPosts { get; set; }
}
