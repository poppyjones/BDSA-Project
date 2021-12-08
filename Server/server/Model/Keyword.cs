namespace Model;

public class Keyword
{
    [Key]
    public int KeywordId { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<Post> Posts { get; set; }
}
