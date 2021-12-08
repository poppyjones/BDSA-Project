
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class Post
{
    [Key]
    public int PostId { get; set; }

    [StringLength(50)]
    public string Title { get; set; }

    [Required]
    public Supervisor Author { get; set; }

    public string Status { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public ICollection<Keyword> Keywords { get; set; }

    public ICollection<Supervisor> CollaboratingUsers { get; set; }

    public Post()
    {
        this.Keywords = new HashSet<Keyword>();
        this.CollaboratingUsers = new HashSet<Supervisor>();   
    }
}
