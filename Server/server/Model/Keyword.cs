namespace server.Model;

public class Keyword
{
    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}
