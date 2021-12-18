namespace server.Model;

public class UserPost
{
    [Key, ForeignKey("UserId")]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [Key, ForeignKey("PostId")]
    public int PostId { get; set; }
    public Post Post { get; set; }

}