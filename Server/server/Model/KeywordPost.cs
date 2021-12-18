namespace server.Model;

public class KeywordPost
{
    [Key, ForeignKey("KeywordId")]
    public int KeywordId { get; set; }
    public Keyword Keyword { get; set; }

    [Key, ForeignKey("PostId")]
    public int PostId { get; set; }
    public Post Post { get; set; }
}