namespace server.Model;

public class KeywordPost
{
    //[Key, ForeignKey("KeywordId")]
    [ForeignKey("KeywordId")]
    //[Key]
    public int KeywordId { get; set; }
    public Keyword Keyword { get; set; }

    //[Key, ForeignKey("PostId")]
    [ForeignKey("PostId")]
    //[Key]
    public int PostId { get; set; }
    public Post Post { get; set; }
}