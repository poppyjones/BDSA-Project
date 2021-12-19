namespace server.Model;

public class Keyword
{
    [Key]
    public int KeywordId { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }

    //[ForeignKey("PostId")]
    public ICollection<Post> Posts { get; set; }

    public List<KeywordPost> KeywordPosts { get; set; }
}