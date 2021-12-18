namespace server.Model;

public class Post
{
    [Key]
    public int PostId { get; set; }

    [StringLength(50)]
    public string Title { get; set; }

    public int AuthorId { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Ended { get; set; }

    public string Status { get; set; }

    [StringLength(500)]
    public string Description { get; set; }



    //[ForeignKey("KeywordId")]
    public ICollection<Keyword> Keywords { get; set; }

    public List<KeywordPost> KeywordPost { get; set; }


    //[ForeignKey("UserId")]
    public virtual ICollection<User> Users { get; set; }

   public List<UserPost> UserPost { get; set; }
}