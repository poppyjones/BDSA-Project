namespace Model;

public class Keyword
{
    // public Keyword(string name, int id)
    // {
    //     this.name = name;
    //     this.id = id;
    // }

    [Key]
    public int Id { get; set; }
    
    [StringLength(50)]
    public string Name { get; set; }

    public ICollection<PostKeyword> PostKeyword { get; set; }
}
