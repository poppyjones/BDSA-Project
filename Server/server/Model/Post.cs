
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class Post
{
    // public Post(Int id, string title, User creator, DateTime created)
    // {
    //     Id = id;
    //     collaborators = new HashSet<User>();
    // }
    // public Post(int id, string title, User creator, DateTime created, ICollection<User> collaborators, string status, string description, Collection<Keyword> keywords)
    // {
    //     this.id = id;
    //     this.title = title;
    //     this.creator = creator;
    //     this.created = created;
    //     this.collaborators = collaborators;
    //     this.status = status;
    //     this.description = description;
    //     this.keywords = keywords;
    // }

    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Title { get; set; }

    [NotMapped]
    public User Creator { get; set; }

    //[DataType(DataType.DateTime)]
    //public DateTime Created { get; set; }

    //[DataType(DataType.DateTime)]
    //public DateTime? Ended { get; set; }

    public string Status { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public ICollection<Keyword> Keywords { get; set; }

    public ICollection<User> Users { get; set; }
}
