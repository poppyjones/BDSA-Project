using System.ComponentModel.DataAnnotations.Schema;

namespace Model;


public class Supervisor
{   
    [Key]
    public int SupervisorId { get; set; }

    public string Name { get; set; }
    
    [EmailAddress]
    public string Email { get; set; }

    public string Institution { get; set; }

    public string Degree { get; set; }

    public ICollection<Post> CollaboratingPosts { get; set; } // Marksu sagde det skulle hedde det >:(

    public ICollection<Post> OwnedPosts { get; set; }

    public Supervisor()
    {
        this.CollaboratingPosts = new HashSet<Post>();  
        this.OwnedPosts = new HashSet<Post>();
    }
}